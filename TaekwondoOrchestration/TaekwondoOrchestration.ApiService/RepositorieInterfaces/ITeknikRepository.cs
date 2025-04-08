using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public interface ITeknikRepository
    {
        Task<List<Teknik>> GetAllTekniksAsync();
        Task<Teknik?> GetTeknikByIdAsync(int id);
        Task<Teknik> CreateTeknikAsync(Teknik teknik);
        Task<bool> DeleteTeknikAsync(int id);
        Task<bool> UpdateTeknikAsync(Teknik teknik);

        // Add the new methods for the TeknikService
        Task<List<Teknik>> GetTekniksByPensumAsync(int pensumId);
        Task<Teknik?> GetTeknikByTeknikNavnAsync(string teknikNavn);
    }
}
