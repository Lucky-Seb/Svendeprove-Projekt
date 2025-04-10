using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IOrdbogRepository
    {
        Task<List<Ordbog>> GetAllOrdbogAsync();
        Task<Ordbog?> GetOrdbogByIdAsync(Guid ordbogId);
        Task<Ordbog> CreateOrdbogAsync(Ordbog ordbog);
        Task<bool> UpdateOrdbogAsync(Ordbog ordbog);
        Task<bool> DeleteOrdbogAsync(Guid ordbogId);

        // Add methods for searching by DanskOrd or KoranOrd
        Task<Ordbog?> GetOrdbogByDanskOrdAsync(string danskOrd);
        Task<Ordbog?> GetOrdbogByKoranOrdAsync(string koranOrd);
    }
}
