using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class BrugerProgramRepository : IBrugerProgramRepository
    {
        private readonly ApiDbContext _context;

        public BrugerProgramRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<BrugerProgram>> GetAllBrugerProgramsAsync()
        {
            return await _context.BrugerProgrammer.ToListAsync();
        }

        public async Task<BrugerProgram?> GetBrugerProgramByIdAsync(int brugerId, int programId)
        {
            return await _context.BrugerProgrammer
                .FirstOrDefaultAsync(bp => bp.BrugerID == brugerId && bp.ProgramID == programId);
        }

        public async Task<BrugerProgram> CreateBrugerProgramAsync(BrugerProgram brugerProgram)
        {
            _context.BrugerProgrammer.Add(brugerProgram);
            await _context.SaveChangesAsync();
            return brugerProgram;
        }

        public async Task<bool> DeleteBrugerProgramAsync(int brugerId, int programId)
        {
            var brugerProgram = await _context.BrugerProgrammer
                .FirstOrDefaultAsync(bp => bp.BrugerID == brugerId && bp.ProgramID == programId);
            if (brugerProgram == null)
                return false;

            _context.BrugerProgrammer.Remove(brugerProgram);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
