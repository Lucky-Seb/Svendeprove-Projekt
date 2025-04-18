using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class ProgramPlanRepository : IProgramPlanRepository
    {
        private readonly ApiDbContext _context;

        public ProgramPlanRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramPlan>> GetAllProgramPlansAsync()
        {
            return await _context.ProgramPlans
                .ToListAsync();
        }

        public async Task<ProgramPlan?> GetProgramPlanByIdAsync(Guid programPlanId)
        {
            return await _context.ProgramPlans
                .FirstOrDefaultAsync(pp => pp.ProgramID == programPlanId);
        }

        public async Task<ProgramPlan?> GetProgramPlanByIdIncludingDeletedAsync(Guid programPlanId)
        {
            return await _context.ProgramPlans
                .IgnoreQueryFilters()  // Ignore global filters (soft delete) for this query
                .FirstOrDefaultAsync(pp => pp.ProgramID == programPlanId);
        }

        public async Task<List<ProgramPlan>> GetAllProgramPlansIncludingDeletedAsync()
        {
            return await _context.ProgramPlans
                .IgnoreQueryFilters()  // Ignore soft delete filter
                .ToListAsync();
        }

        public async Task<ProgramPlan> CreateProgramPlanAsync(ProgramPlan programPlan)
        {
            _context.ProgramPlans.Add(programPlan);
            await _context.SaveChangesAsync();
            return programPlan;
        }

        public async Task<bool> UpdateProgramPlanAsync(ProgramPlan programPlan)
        {
            _context.Entry(programPlan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProgramPlanAsync(Guid programPlanId)
        {
            var programPlan = await _context.ProgramPlans.FindAsync(programPlanId);
            if (programPlan == null)  // Check if already deleted
                return false;

            _context.ProgramPlans.Remove(programPlan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProgramPlan>> GetAllProgramPlansByKlubIdAsync(Guid klubId)
        {
            // Get the Program IDs associated with the Klub
            var programIds = await _context.KlubProgrammer
                .Where(kp => kp.KlubID == klubId)
                .Select(kp => kp.ProgramID)
                .ToListAsync();

            // Get all the program plans related to the Program IDs
            var programPlans = await _context.ProgramPlans
                .Where(pp => programIds.Contains(pp.ProgramID))
                .Include(pp => pp.Træninger) // Include training sessions
                    .ThenInclude(t => t.Quiz) // Include single Quiz
                .Include(pp => pp.Træninger)
                    .ThenInclude(t => t.Teori) // Include single Teori
                .Include(pp => pp.Træninger)
                    .ThenInclude(t => t.Teknik) // Include single Teknik
                .Include(pp => pp.Træninger)
                    .ThenInclude(t => t.Øvelse) // Include single Øvelse
                .ToListAsync();

            return programPlans;
        }

        public async Task<List<ProgramPlan>> GetAllProgramPlansByBrugerIdAsync(Guid brugerId)
        {
            // Get the Program IDs associated with the Bruger (user)
            var programIds = await _context.BrugerProgrammer
                .Where(bp => bp.BrugerID == brugerId)
                .Select(bp => bp.ProgramID)
                .ToListAsync();

            // Get all the program plans related to the Program IDs
            var programPlans = await _context.ProgramPlans
                .Where(pp => programIds.Contains(pp.ProgramID))
                .Include(pp => pp.Træninger) // Include training sessions
                    .ThenInclude(t => t.Quiz) // Include single Quiz
                .Include(pp => pp.Træninger)
                    .ThenInclude(t => t.Teori) // Include single Teori
                .Include(pp => pp.Træninger)
                    .ThenInclude(t => t.Teknik) // Include single Teknik
                .Include(pp => pp.Træninger)
                    .ThenInclude(t => t.Øvelse) // Include single Øvelse
                .ToListAsync();

            return programPlans;
        }

        public async Task<ProgramPlan?> UpdateProgramPlanIncludingDeletedAsync(Guid programPlanId, ProgramPlan programPlan)
        {
            var existingProgramPlan = await _context.ProgramPlans
                .IgnoreQueryFilters()  // Ignore soft delete filter
                .FirstOrDefaultAsync(pp => pp.ProgramID == programPlanId);

            if (existingProgramPlan == null) return null;

            _context.Entry(existingProgramPlan).CurrentValues.SetValues(programPlan);
            await _context.SaveChangesAsync();
            return existingProgramPlan;
        }
    }
}
