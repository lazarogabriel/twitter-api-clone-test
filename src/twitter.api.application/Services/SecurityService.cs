using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using twitter.api.application.Models.Security;
using twitter.api.application.Services.Abstractions;
using twitter.api.data.DbContexts;
using twitter.api.domain.Models;

namespace twitter.api.application.Services
{
    public class SecurityService : ISecurityService
    {
        #region Fields

        private readonly ITwitterApiDbContext _dbContext;
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor

        public SecurityService(ITwitterApiDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        #endregion

        #region Public Methods

        public async Task<TokenResult> RegisterAsync(RegisterCommand request)
        {
            // Puedes agregar validaciones, hashing, etc.
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Password = request.Password, // ❗ Reemplazar por hash real
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return GenerateToken(user);
        }

        public async Task<TokenResult> LoginAsync(LoginCommand request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u =>
                u.UserName == request.UserName &&
                u.Password == request.Password); // ❗ Igual: usar hashing

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            return GenerateToken(user);
        }



        #endregion

        #region Private Methods

        private TokenResult GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddHours(3);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResult(
                token: tokenString,
                expirationTime: expires,
                refreshToken: Guid.NewGuid().ToString()); // Opcional: persistir si querés permitir refresh real
        }

        #endregion

    }
}

