using System;
using System.Threading.Tasks;
using twitter.api.application.Models.Security;

namespace twitter.api.application.Services.Abstractions
{
    public interface ISecurityService
    {
        Task<TokenResult> Login(LoginCommand command, bool validatePassword = true);

        Task<TokenResult> Register(RegisterCommand command);

        Task<TokenResult> RefreshToken(Guid refreshToken);
    }
}
