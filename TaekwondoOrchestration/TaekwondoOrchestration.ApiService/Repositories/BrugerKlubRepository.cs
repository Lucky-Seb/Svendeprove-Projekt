using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;


namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class BrugerKlubRepository : IBrugerKlubRepository
    {
        private readonly ApiDbContext _context;

        public BrugerKlubRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<BrugerKlub>> GetAllBrugerKlubberAsync()
        {
            return await _context.BrugerKlubber.ToListAsync();
        }

        public async Task<BrugerKlub?> GetBrugerKlubByIdAsync(Guid brugerId, Guid klubId)
        {
            return await _context.BrugerKlubber
                .FirstOrDefaultAsync(bk => bk.BrugerID == brugerId && bk.KlubID == klubId);
        }

        public async Task<BrugerKlub> CreateBrugerKlubAsync(BrugerKlub brugerKlub)
        {
            _context.BrugerKlubber.Add(brugerKlub);
            await _context.SaveChangesAsync();
            return brugerKlub;
        }

        public async Task<bool> DeleteBrugerKlubAsync(Guid brugerId, Guid klubId)
        {
            var brugerKlub = await _context.BrugerKlubber
                .FirstOrDefaultAsync(bk => bk.BrugerID == brugerId && bk.KlubID == klubId);
            if (brugerKlub == null)
                return false;

            _context.BrugerKlubber.Remove(brugerKlub);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
