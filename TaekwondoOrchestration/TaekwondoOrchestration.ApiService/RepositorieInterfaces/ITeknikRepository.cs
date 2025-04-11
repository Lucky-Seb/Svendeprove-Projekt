using TaekwondoApp.Shared.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface ITeknikRepository
    {
        Task<List<Teknik>> GetAllTekniksAsync();
        Task<Teknik?> GetTeknikByIdAsync(Guid teknikId);
        Task<Teknik> CreateTeknikAsync(Teknik teknik);
        Task<bool> DeleteTeknikAsync(Guid teknikId);
        Task<bool> UpdateTeknikAsync(Teknik teknik);

        // Add the new methods for the TeknikService
        Task<List<Teknik>> GetTekniksByPensumAsync(Guid pensumId);
        Task<Teknik?> GetTeknikByTeknikNavnAsync(string teknikNavn);
    }
}
