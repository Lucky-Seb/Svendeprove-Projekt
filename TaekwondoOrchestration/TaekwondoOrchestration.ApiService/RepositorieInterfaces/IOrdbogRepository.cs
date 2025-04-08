using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public interface IOrdbogRepository
    {
        Task<List<Ordbog>> GetAllOrdbogAsync();
        Task<Ordbog?> GetOrdbogByIdAsync(int id);
        Task<Ordbog> CreateOrdbogAsync(Ordbog ordbog);
        Task<bool> UpdateOrdbogAsync(Ordbog ordbog);
        Task<bool> DeleteOrdbogAsync(int id);

        // Add methods for searching by DanskOrd or KoranOrd
        Task<Ordbog?> GetOrdbogByDanskOrdAsync(string danskOrd);
        Task<Ordbog?> GetOrdbogByKoranOrdAsync(string koranOrd);
    }
}
