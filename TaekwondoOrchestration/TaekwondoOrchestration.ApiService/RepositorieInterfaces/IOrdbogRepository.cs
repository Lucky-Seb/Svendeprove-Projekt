using TaekwondoApp.Shared.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IOrdbogRepository
    {
        Task<List<Ordbog>> GetAllOrdbogAsync();
        Task<Ordbog?> GetOrdbogByIdAsync(Guid ordbogId);
        Task<Ordbog> CreateOrdbogAsync(Ordbog ordbog);
        Task<bool> UpdateOrdbogAsync(Ordbog ordbog);
        Task<bool> DeleteOrdbogAsync(Guid ordbogId);
        Task<bool> UpdateAsync(Ordbog ordbog);

        Task<List<Ordbog>> GetAllOrdbogIncludingDeletedAsync();

        // Add methods for searching by DanskOrd or KoranOrd
        Task<Ordbog?> GetOrdbogByDanskOrdAsync(string danskOrd);
        Task<Ordbog?> GetOrdbogByKoranskOrdAsync(string koranOrd);
        Task<Ordbog?> UpdateOrdbogIncludingDeletedAsync(Guid id, Ordbog ordbog);
        Task<Ordbog?> GetOrdbogByIdIncludingDeletedAsync(Guid id);

    }
}
