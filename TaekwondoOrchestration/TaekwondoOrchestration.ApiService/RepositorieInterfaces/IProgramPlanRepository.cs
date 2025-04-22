using TaekwondoApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IProgramPlanRepository
    {
        Task<List<ProgramPlan>> GetAllProgramPlansAsync();
        Task<ProgramPlan?> GetProgramPlanByIdAsync(Guid programPlanId);
        Task<ProgramPlan?> GetProgramPlanByIdIncludingDeletedAsync(Guid programPlanId);
        Task<List<ProgramPlan>> GetAllProgramPlansIncludingDeletedAsync();
        Task<ProgramPlan> CreateProgramPlanAsync(ProgramPlan programPlan);
        Task<bool> UpdateProgramPlanAsync(ProgramPlan programPlan);
        Task<bool> DeleteProgramPlanAsync(Guid programPlanId);
        Task<List<ProgramPlan>> GetAllProgramPlansByBrugerIdAsync(Guid brugerId);
        Task<List<ProgramPlan>> GetAllProgramPlansByKlubIdAsync(Guid klubId);
        Task<ProgramPlan?> UpdateProgramPlanIncludingDeletedAsync(Guid programPlanId, ProgramPlan programPlan);
        Task<ProgramPlan?> GetProgramPlanWithDetailsAsync(Guid programPlanId);
    }
}
