using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class ØvelseRepository : IØvelseRepository
    {
        private readonly ApiDbContext _context;

        public ØvelseRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Øvelse>> GetAllØvelserAsync()
        {
            return await _context.Øvelser.ToListAsync();
        }

        public async Task<Øvelse?> GetØvelseByIdAsync(Guid øvelseId)
        {
            return await _context.Øvelser.FindAsync(øvelseId);
        }

        public async Task<List<Øvelse>> GetØvelserBySværhedAsync(string sværhed)
        {
            return await _context.Øvelser.Where(o => o.ØvelseSværhed == sværhed).ToListAsync();
        }

        // Method to fetch Øvelser by BrugerID
        public async Task<List<Øvelse>> GetØvelserByBrugerAsync(Guid brugerId)
        {
            return await _context.Øvelser
                .Where(o => o.BrugerØvelses.Any(b => b.BrugerID == brugerId))
                .Include(o => o.BrugerØvelses) // Include the related BrugerØvelse
                .ThenInclude(b => b.Bruger)    // Include the related Bruger
                .ToListAsync();
        }

        public async Task<List<Øvelse>> GetØvelserByKlubAsync(Guid klubId)
        {
            return await _context.Øvelser
                .Where(o => o.KlubØvelses.Any(k => k.KlubID == klubId))
                .Include(o => o.KlubØvelses)   // Include the related KlubØvelse
                .ThenInclude(k => k.Klub)     // Include the related Klub
                .ToListAsync();
        }


        public async Task<List<Øvelse>> GetØvelserByNavnAsync(string navn)
        {
            return await _context.Øvelser.Where(o => o.ØvelseNavn.Contains(navn)).ToListAsync();
        }

        public async Task<Øvelse> CreateØvelseAsync(Øvelse øvelse)
        {
            _context.Øvelser.Add(øvelse);
            await _context.SaveChangesAsync();
            return øvelse;
        }

        public async Task<bool> DeleteØvelseAsync(Guid øvelseId)
        {
            var øvelse = await _context.Øvelser.FindAsync(øvelseId);
            if (øvelse == null)
                return false;

            _context.Øvelser.Remove(øvelse);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateØvelseAsync(Øvelse øvelse)
        {
            _context.Entry(øvelse).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
