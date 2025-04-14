using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.DTO;
namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<LoginResultDTO> LoginAsync(string username, string password);
        Task<string> Verify2FACodeAsync(string email, string code);
        Task<string> OAuthCallbackAsync(string provider, string email);
        Task<string> Generate2FAQRCodeAsync(string username);
        Task<bool> Verify2FASetupAsync(string email, string code);
        Task<Bruger> RegisterLocalAsync(string username, string password);
    }
}
