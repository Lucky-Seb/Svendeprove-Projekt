using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IProgramPlanService
    {
        #region CRUD Operations

        Task<Result<ProgramPlanDTO>> CreateProgramPlanWithBrugerOrKlubAsync(ProgramPlanDTO dto);

        Task<Result<ProgramPlanDTO>> UpdateProgramPlanWithBrugerOrKlubAsync(Guid id, ProgramPlanDTO dto);
        Task<Result<ProgramPlanDTO>> UpdateProgramPlanAsync(Guid programId, ProgramPlanDTO updatedDto);
        Task<Result<bool>> DeleteProgramPlanAsync(Guid id);

        #endregion

        #region Get Operations

        // Get all programs with their training, quiz, theory, technique, and exercise
        Task<Result<IEnumerable<ProgramPlanDTO>>> GetAllProgramsAsync();
        Task<Result<IEnumerable<ProgramPlanDTO>>> GetAllProgramPlansAsync();

        Task<Result<ProgramPlanDTO>> GetProgramPlanByIdAsync(Guid id);
        Task<Result<ProgramPlanDTO>> GetProgramPlanWithDetailsAsync(Guid id);

        // Get programs by user (Bruger)
        Task<Result<IEnumerable<ProgramPlanDTO>>> GetAllProgramPlansByBrugerIdAsync(Guid brugerId);

        // Get programs by club (Klub)
        Task<Result<IEnumerable<ProgramPlanDTO>>> GetAllProgramPlansByKlubIdAsync(Guid klubId);
        Task<Result<IEnumerable<ProgramPlanDTO>>> GetFilteredProgramPlansAsync(Guid? brugerId, List<Guid> klubIds);

        #endregion
    }
}
