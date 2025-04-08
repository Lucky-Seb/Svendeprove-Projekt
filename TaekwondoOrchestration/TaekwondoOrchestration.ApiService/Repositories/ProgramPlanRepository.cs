using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
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

        public async Task<List<ProgramPlan>> GetAllAsync()
        {
            return await _context.ProgramPlans.ToListAsync();
        }

        public async Task<ProgramPlan?> GetByIdAsync(int id)
        {
            return await _context.ProgramPlans.FindAsync(id);
        }

        public async Task<ProgramPlan> CreateAsync(ProgramPlan programPlan)
        {
            _context.ProgramPlans.Add(programPlan);
            await _context.SaveChangesAsync();
            return programPlan;
        }
        public async Task<bool> UpdateAsync(ProgramPlan programPlan)
        {
            _context.Entry(programPlan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var programPlan = await _context.ProgramPlans.FindAsync(id);
            if (programPlan == null) return false;

            _context.ProgramPlans.Remove(programPlan);
            await _context.SaveChangesAsync();
            return true;
        }
        // Get all training sessions by KlubID
        public async Task<List<ProgramPlan>> GetAllByKlubIdAsync(int klubId)
        {
            // Get the Program IDs associated with the Klub
            var programIds = await _context.KlubProgrammer
                .Where(kp => kp.KlubID == klubId)
                .Select(kp => kp.ProgramID)
                .ToListAsync();

            // Get all the training sessions related to the Program IDs
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

        // Get all training sessions by BrugerID
        public async Task<List<ProgramPlan>> GetAllByBrugerIdAsync(int brugerId)
        {
            // Get the Program IDs associated with the Bruger (user)
            var programIds = await _context.BrugerProgrammer
                .Where(bp => bp.BrugerID == brugerId)
                .Select(bp => bp.ProgramID)
                .ToListAsync();

            // Get all the training sessions related to the Program IDs
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
    }
}
