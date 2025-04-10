using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IBrugerProgramRepository
    {
        Task<List<BrugerProgram>> GetAllBrugerProgramsAsync();
        Task<BrugerProgram?> GetBrugerProgramByIdAsync(Guid brugerId, Guid programId);
        Task<BrugerProgram> CreateBrugerProgramAsync(BrugerProgram brugerProgram);
        Task<bool> DeleteBrugerProgramAsync(Guid brugerId, Guid programId);
    }
}
