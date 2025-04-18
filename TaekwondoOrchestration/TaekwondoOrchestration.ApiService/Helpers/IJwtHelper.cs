using TaekwondoApp.Shared.Models;

namespace TaekwondoOrchestration.ApiService.Helpers
{
    public interface IJwtHelper
    {
        string GenerateToken(Bruger bruger);
    }
}