using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IProgramPlanRepository
    {
        Task<List<ProgramPlan>> GetAllAsync();
        Task<ProgramPlan?> GetByIdAsync(int id);
        Task<ProgramPlan> CreateAsync(ProgramPlan programPlan);
        Task<bool> UpdateAsync(ProgramPlan programPlan);
        Task<bool> DeleteAsync(int id);
        Task<List<ProgramPlan>> GetAllByBrugerIdAsync(int brugerId);

        // Get all training sessions by Klub (Club)
        Task<List<ProgramPlan>> GetAllByKlubIdAsync(int klubId);
    }
}
