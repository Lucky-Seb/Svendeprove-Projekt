using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public interface IBrugerProgramRepository
    {
        Task<List<BrugerProgram>> GetAllBrugerProgramsAsync();
        Task<BrugerProgram?> GetBrugerProgramByIdAsync(int brugerId, int programId);
        Task<BrugerProgram> CreateBrugerProgramAsync(BrugerProgram brugerProgram);
        Task<bool> DeleteBrugerProgramAsync(int brugerId, int programId);
    }
}
