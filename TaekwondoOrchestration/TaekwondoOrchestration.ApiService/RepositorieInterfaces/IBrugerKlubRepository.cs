using TaekwondoApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IBrugerKlubRepository
    {
        Task<List<BrugerKlub>> GetAllBrugerKlubberAsync();
        Task<BrugerKlub?> GetBrugerKlubByIdAsync(Guid brugerId, Guid klubId);
        Task<BrugerKlub> CreateBrugerKlubAsync(BrugerKlub brugerKlub);
        Task<bool> DeleteBrugerKlubAsync(Guid brugerId, Guid klubId);
    }
}
