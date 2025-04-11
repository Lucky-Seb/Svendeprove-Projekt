using TaekwondoApp.Shared.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IKlubProgramRepository
    {
        Task<List<KlubProgram>> GetAllKlubProgrammerAsync();
        Task<KlubProgram?> GetKlubProgramByIdAsync(Guid klubId, Guid programId);
        Task<KlubProgram> CreateKlubProgramAsync(KlubProgram klubProgram);
        Task<bool> DeleteKlubProgramAsync(Guid klubId, Guid programId);
    }
}
