using TaekwondoApp.Shared.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface ITræningRepository
    {
        Task<IEnumerable<Træning>> GetByProgramIdAsync(Guid programId);
        Task<List<Træning>> GetAllTræningAsync();
        Task<Træning> GetTræningByIdAsync(Guid træningId);
        Task<Træning> CreateTræningAsync(Træning træning);
        Task<bool> DeleteTræningAsync(Guid træningId);
        Task<bool> UpdateTræningAsync(Træning træning);
    }
}
