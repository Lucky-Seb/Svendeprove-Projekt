using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface ITræningRepository
    {
        Task<IEnumerable<Træning>> GetByProgramIdAsync(int id);
        Task<List<Træning>> GetAllTræningAsync();
        Task<Træning> GetTræningByIdAsync(int id);
        Task<Træning> CreateTræningAsync(Træning træning);
        Task<bool> DeleteTræningAsync(int id);
        Task<bool> UpdateTræningAsync(Træning træning);
    }
}
