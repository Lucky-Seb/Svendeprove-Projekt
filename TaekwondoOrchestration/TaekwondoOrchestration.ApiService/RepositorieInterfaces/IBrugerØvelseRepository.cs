using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{ 
    public interface IBrugerØvelseRepository
    {
        Task<List<BrugerØvelse>> GetAllBrugerØvelserAsync();
        Task<BrugerØvelse?> GetBrugerØvelseByIdAsync(Guid brugerId, Guid øvelseId);
        Task<BrugerØvelse> CreateBrugerØvelseAsync(BrugerØvelse brugerØvelse);
        Task<bool> DeleteBrugerØvelseAsync(Guid brugerId, Guid øvelseId);
    }
}
