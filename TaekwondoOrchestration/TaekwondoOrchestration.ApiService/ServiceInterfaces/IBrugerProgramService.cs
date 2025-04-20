using TaekwondoApp.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IBrugerProgramService
    {
        // Asynchronously retrieves a list of all BrugerPrograms
        Task<List<BrugerProgramDTO>> GetAllBrugerProgramsAsync();

        // Asynchronously retrieves a BrugerProgram by its BrugerID and ProgramID
        Task<BrugerProgramDTO?> GetBrugerProgramByIdAsync(Guid brugerId, Guid programId);

        // Asynchronously creates a new BrugerProgram and returns the created DTO
        Task<BrugerProgramDTO?> CreateBrugerProgramAsync(BrugerProgramDTO brugerProgramDto);

        // Asynchronously deletes a BrugerProgram by its BrugerID and ProgramID
        Task<bool> DeleteBrugerProgramAsync(Guid brugerId, Guid programId);
    }
}
