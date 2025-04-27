using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IBrugerProgramService
    {
        // Get all BrugerPrograms
        Task<Result<IEnumerable<BrugerProgramDTO>>> GetAllBrugerProgramsAsync();

        // Get BrugerProgram by ID
        Task<Result<BrugerProgramDTO>> GetBrugerProgramByIdAsync(Guid brugerId, Guid programId);

        // Create a new BrugerProgram
        Task<Result<BrugerProgramDTO>> CreateBrugerProgramAsync(BrugerProgramDTO brugerProgramDto);

        // Delete a BrugerProgram
        Task<Result<bool>> DeleteBrugerProgramAsync(Guid brugerId, Guid programId);
    }
}
