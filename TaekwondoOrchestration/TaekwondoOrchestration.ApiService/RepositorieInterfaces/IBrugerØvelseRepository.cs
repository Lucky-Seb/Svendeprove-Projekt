using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.Repositories
{
    public interface IBrugerØvelseRepository
    {
        Task<List<BrugerØvelse>> GetAllBrugerØvelserAsync();
        Task<BrugerØvelse?> GetBrugerØvelseByIdAsync(int brugerId, int øvelseId);
        Task<BrugerØvelse> CreateBrugerØvelseAsync(BrugerØvelse brugerØvelse);
        Task<bool> DeleteBrugerØvelseAsync(int brugerId, int øvelseId);
    }
}
