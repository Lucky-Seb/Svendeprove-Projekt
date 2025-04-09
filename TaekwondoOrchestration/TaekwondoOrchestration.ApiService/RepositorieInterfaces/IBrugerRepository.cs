using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IBrugerRepository
    {
        // Existing methods
        Task<List<Bruger>> GetAllBrugereAsync();
        Task<Bruger?> GetBrugerByIdAsync(int id);
        Task<List<Bruger>> GetBrugerByRoleAsync(string role);
        Task<List<Bruger>> GetBrugerByBælteAsync(string bæltegrad);
        Task<List<BrugerDTO>> GetBrugereByKlubAsync(int klubId);
        Task<List<BrugerDTO>> GetBrugereByKlubAndBæltegradAsync(int klubId, string bæltegrad);
        Task<Bruger?> GetBrugerByBrugernavnAsync(string brugernavn);
        Task<List<Bruger>> GetBrugerByFornavnEfternavnAsync(string fornavn, string efternavn);
        Task<Bruger> CreateBrugerAsync(Bruger bruger);
        Task<bool> UpdateBrugerAsync(Bruger bruger);

        // Add DeleteBrugerAsync method
        Task<bool> DeleteBrugerAsync(int id);
    }
}
