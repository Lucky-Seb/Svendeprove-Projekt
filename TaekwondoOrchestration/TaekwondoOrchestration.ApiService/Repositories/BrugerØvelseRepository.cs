using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class BrugerØvelseRepository : IBrugerØvelseRepository
    {
        private readonly ApiDbContext _context;

        public BrugerØvelseRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<BrugerØvelse>> GetAllBrugerØvelserAsync()
        {
            return await _context.BrugerØvelser.ToListAsync();
        }

        public async Task<BrugerØvelse?> GetBrugerØvelseByIdAsync(Guid brugerId, Guid øvelseId)
        {
            return await _context.BrugerØvelser
                .FirstOrDefaultAsync(bo => bo.BrugerID == brugerId && bo.ØvelseID == øvelseId);
        }

        public async Task<BrugerØvelse> CreateBrugerØvelseAsync(BrugerØvelse brugerØvelse)
        {
            _context.BrugerØvelser.Add(brugerØvelse);
            await _context.SaveChangesAsync();
            return brugerØvelse;
        }

        public async Task<bool> DeleteBrugerØvelseAsync(Guid brugerId, Guid øvelseId)
        {
            var brugerØvelse = await _context.BrugerØvelser
                .FirstOrDefaultAsync(bo => bo.BrugerID == brugerId && bo.ØvelseID == øvelseId);
            if (brugerØvelse == null)
                return false;

            _context.BrugerØvelser.Remove(brugerØvelse);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
