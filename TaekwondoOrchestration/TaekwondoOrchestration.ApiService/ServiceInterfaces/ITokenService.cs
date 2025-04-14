using TaekwondoApp.Shared.Models;
namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(Bruger Bruger);
    }
}
