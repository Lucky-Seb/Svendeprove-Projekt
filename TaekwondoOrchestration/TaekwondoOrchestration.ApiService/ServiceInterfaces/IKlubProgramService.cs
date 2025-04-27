using TaekwondoApp.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IKlubProgramService
    {
        // Get all KlubProgrammer
        Task<Result<IEnumerable<KlubProgramDTO>>> GetAllKlubProgrammerAsync();

        // Get a specific KlubProgram by its IDs (KlubID, ProgramID)
        Task<Result<KlubProgramDTO>> GetKlubProgramByIdAsync(Guid klubId, Guid programId);

        // Create a new KlubProgram
        Task<Result<KlubProgramDTO>> CreateKlubProgramAsync(KlubProgramDTO klubProgramDto);

        // Delete a KlubProgram by its IDs (KlubID, ProgramID)
        Task<Result<bool>> DeleteKlubProgramAsync(Guid klubId, Guid programId);
    }
}
