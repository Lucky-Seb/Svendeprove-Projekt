using TaekwondoApp.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IProgramPlanService
    {
        // CRUD Operations
        Task<List<ProgramPlanDTO>> GetAllProgramPlansAsync();
        Task<ProgramPlanDTO?> GetProgramPlanByIdAsync(Guid id);
        Task<ProgramPlanDTO> CreateProgramPlanWithBrugerAndKlubAsync(ProgramPlanDTO dto);
        Task<ProgramPlanDTO> UpdateProgramPlanWithBrugerAndKlubAsync(Guid id, ProgramPlanDTO dto);
        Task<bool> UpdateProgramPlanAsync(Guid id, ProgramPlanDTO programPlanDto);
        Task<bool> DeleteProgramPlanAsync(Guid id);

        // Get all programs with their training, quiz, theory, technique, and exercise
        Task<List<ProgramPlanDTO>> GetAllProgramsAsync();
        Task<ProgramPlanDTO?> GetProgramByIdAsync(Guid id);

        // Get programs by user or club
        Task<List<ProgramPlanDTO>> GetAllProgramPlansByBrugerIdAsync(Guid brugerId);
        Task<List<ProgramPlanDTO>> GetAllProgramPlansByKlubIdAsync(Guid klubId);
    }
}
