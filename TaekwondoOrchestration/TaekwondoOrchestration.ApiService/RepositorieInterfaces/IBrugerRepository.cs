using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IBrugerRepository
    {
        // Existing methods
        Task<List<Bruger>> GetAllBrugereAsync();
        Task<Bruger?> GetBrugerByIdAsync(Guid brugerId);
        Task<List<Bruger>> GetBrugerByRoleAsync(string role);
        Task<List<Bruger>> GetBrugerByBælteAsync(string bæltegrad);
        Task<List<BrugerDTO>> GetBrugereByKlubAsync(Guid klubId);
        Task<List<BrugerDTO>> GetBrugereByKlubAndBæltegradAsync(Guid klubId, string bæltegrad);
        Task<Bruger?> GetBrugerByBrugernavnAsync(string brugernavn);
        Task<List<Bruger>> GetBrugerByFornavnEfternavnAsync(string fornavn, string efternavn);
        Task<Bruger> CreateBrugerAsync(Bruger bruger);
        Task<bool> UpdateBrugerAsync(Bruger bruger);
        Task<Bruger?> GetBrugerByIdIncludingDeletedAsync(Guid id);

        // Add DeleteBrugerAsync method
        Task<bool> DeleteBrugerAsync(Guid brugerId);
        Task<BrugerDTO?> AuthenticateBrugerAsync(string emailOrBrugernavn, string brugerkode);
    }
}
