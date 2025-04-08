using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class KlubProgramRepository : IKlubProgramRepository
    {
        private readonly ApiDbContext _context;

        public KlubProgramRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<KlubProgram>> GetAllKlubProgrammerAsync()
        {
            return await _context.KlubProgrammer.ToListAsync();
        }

        public async Task<KlubProgram?> GetKlubProgramByIdAsync(int klubId, int programId)
        {
            return await _context.KlubProgrammer
                .FirstOrDefaultAsync(k => k.KlubID == klubId && k.ProgramID == programId);
        }

        public async Task<KlubProgram> CreateKlubProgramAsync(KlubProgram klubProgram)
        {
            _context.KlubProgrammer.Add(klubProgram);
            await _context.SaveChangesAsync();
            return klubProgram;
        }

        public async Task<bool> DeleteKlubProgramAsync(int klubId, int programId)
        {
            var klubProgram = await _context.KlubProgrammer
                .FirstOrDefaultAsync(k => k.KlubID == klubId && k.ProgramID == programId);
            if (klubProgram == null)
                return false;

            _context.KlubProgrammer.Remove(klubProgram);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
