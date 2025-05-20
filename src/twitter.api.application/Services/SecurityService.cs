using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using twitter.api.application.Models.Security;
using twitter.api.application.Services.Abstractions;
using twitter.api.data.DbContexts;
using twitter.api.domain.Constants;
using twitter.api.domain.Exceptions;
using twitter.api.domain.Models;
using BCryptClass = BCrypt.Net.BCrypt;

namespace twitter.api.application.Services
{
    public class SecurityService : ISecurityService
    {
        #region Fields

        private readonly ITwitterApiDbContext _dbContext;
        private readonly SecurityServiceConfiguration _securitySettings;

        #endregion

        #region Constructor

        public SecurityService(ITwitterApiDbContext dbContext, IOptions<SecurityServiceConfiguration> securitySettings)
        {
            _dbContext = dbContext;
            _securitySettings = securitySettings.Value;
        }

        #endregion

        #region Public Methods

        public async Task<TokenResult> Register(RegisterCommand command)
        {
            var passwordHash = BCryptClass.HashPassword(command.Password);
            var user = new User(userName: command.UserName);
            var authUser = new AuthUser(email: command.Email, passwordHash: passwordHash, user: user);

            var isEmailRepeated = await _dbContext.AuthUsers.AnyAsync(u => u.Email == authUser.Email);
            if (isEmailRepeated)
            {
                throw new RepeatedElementException(Errors.UserEmailRepeated);
            }

            var isUserNameRepeated = await _dbContext.Users.AnyAsync(u => u.UserName == user.UserName);
            if (isUserNameRepeated)
            {
                throw new RepeatedElementException(Errors.UserNameRepeated);
            }

            _dbContext.AuthUsers.Add(authUser);
            _dbContext.Users.Add(user);

            await _dbContext.CommitAsync();

            return await GenerateToken(authUser);
        }

        public async Task<TokenResult> Login(LoginCommand command, bool validatePassword = true)
        {
            var user = await _dbContext.AuthUsers
                .FirstOrDefaultAsync(u => u.Email == command.Email);

            if (user == null || (validatePassword && !BCryptClass.Verify(command.Password, user.PasswordHash)))
            {
                throw new AuthenticationException(Errors.FailedLogin);
            }

            return await GenerateToken(user);
        }

        public async Task<TokenResult> RefreshToken(Guid refreshToken)
        {
            var token = await _dbContext.RefreshTokens
                .Include(a => a.AuthUser)
                .FirstOrDefaultAsync(a => a.Id == refreshToken);

            // Is the refresh token valid?
            if (token == null || token.ExpiresAt < DateTime.UtcNow)
            {
                throw new AuthenticationException(Errors.InvalidRefreshToken);
            }

            return await Login(
                command: new LoginCommand(email: token.AuthUser.Email, password: string.Empty),
                validatePassword: false);
        }

        #endregion

        #region Private Methods

        private async Task<TokenResult> GenerateToken(AuthUser user)
        {
            var refreshTokensToDelete = _dbContext.RefreshTokens.Where(rf => rf.AuthUser.Id == user.Id);
            _dbContext.RefreshTokens.RemoveRange(refreshTokensToDelete);

            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_securitySettings.AccesTokenDuration);
            var refreshTokenExpiresAt = DateTime.UtcNow.AddMinutes(_securitySettings.RefreshTokenDuration);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securitySettings.Secret!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId", user.UserId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _securitySettings.Issuer,
                audience: _securitySettings.Audience,
                claims: claims,
                expires: accessTokenExpiresAt,
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = new RefreshToken(
                expiresAt: refreshTokenExpiresAt,
                authUser: user);

            _dbContext.RefreshTokens.Add(refreshToken);

            await _dbContext.CommitAsync();

            return new TokenResult(
                 token: tokenString,
                 expiresAt: accessTokenExpiresAt,
                 refreshToken: refreshToken.Id);
        }
       
        #endregion
    }
}

