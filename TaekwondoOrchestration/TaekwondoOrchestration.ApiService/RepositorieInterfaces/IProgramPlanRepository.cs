using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IProgramPlanRepository
    {
        Task<List<ProgramPlan>> GetAllAsync();
        Task<ProgramPlan?> GetByIdAsync(Guid progrmaId);
        Task<ProgramPlan> CreateAsync(ProgramPlan programPlan);
        Task<bool> UpdateAsync(ProgramPlan programPlan);
        Task<bool> DeleteAsync(Guid progrmaPlanId);
        Task<List<ProgramPlan>> GetAllByBrugerIdAsync(Guid brugerId);

        // Get all training sessions by Klub (Club)
        Task<List<ProgramPlan>> GetAllByKlubIdAsync(Guid klubId);
    }
}
