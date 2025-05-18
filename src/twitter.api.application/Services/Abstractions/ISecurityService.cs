using System.Threading.Tasks;
using twitter.api.application.Models.Security;

namespace twitter.api.application.Services.Abstractions
{
    public interface ISecurityService
    {
        Task<TokenResult> LoginAsync(LoginCommand command);

        Task<TokenResult> RegisterAsync(RegisterCommand command);
    }
}
