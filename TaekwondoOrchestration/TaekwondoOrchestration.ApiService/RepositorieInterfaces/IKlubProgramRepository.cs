using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public interface IKlubProgramRepository
    {
        Task<List<KlubProgram>> GetAllKlubProgrammerAsync();
        Task<KlubProgram?> GetKlubProgramByIdAsync(int klubId, int programId);
        Task<KlubProgram> CreateKlubProgramAsync(KlubProgram klubProgram);
        Task<bool> DeleteKlubProgramAsync(int klubId, int programId);
    }
}
