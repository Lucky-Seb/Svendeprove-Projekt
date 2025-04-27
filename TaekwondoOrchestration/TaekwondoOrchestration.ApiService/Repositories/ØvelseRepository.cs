using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await _context.Øvelser
                .ToListAsync();
        }

        public async Task<Øvelse?> GetØvelseByIdAsync(Guid øvelseId)
        {
            return await _context.Øvelser
                .FirstOrDefaultAsync(o => o.ØvelseID == øvelseId);
        }
        public async Task<IEnumerable<Øvelse>> GetFilteredØvelserAsync(Guid? brugerId, List<Guid> klubIds)
        {
            return await _context.Øvelser
                .Where(x =>
                    // Global øvelser
                    (x.BrugerØvelses.All(b => b.BrugerID == null) && x.KlubØvelses.All(k => k.KlubID == null)) ||

                    // Øvelser created by user
                    (brugerId != null && x.BrugerØvelses.Any(b => b.BrugerID == brugerId)) ||

                    // Øvelser from any of the user's clubs
                    (klubIds.Any() && x.KlubØvelses.Any(k => klubIds.Contains(k.KlubID)))
                )
                .ToListAsync();
        }

        public async Task<List<Øvelse>> GetØvelserBySværhedAsync(string sværhed)
        {
            return await _context.Øvelser
                .Where(o => o.ØvelseSværhed == sværhed)  
                .ToListAsync();
        }

        public async Task<List<Øvelse>> GetØvelserByBrugerAsync(Guid brugerId)
        {
            return await _context.Øvelser
                .Where(o => o.BrugerØvelses.Any(b => b.BrugerID == brugerId))
                .Include(o => o.BrugerØvelses)
                .ThenInclude(b => b.Bruger)
                .ToListAsync();
        }

        public async Task<List<Øvelse>> GetØvelserByKlubAsync(Guid klubId)
        {
            return await _context.Øvelser
                .Where(o => o.KlubØvelses.Any(k => k.KlubID == klubId))
                .Include(o => o.KlubØvelses)
                .ThenInclude(k => k.Klub)
                .ToListAsync();
        }

        public async Task<List<Øvelse>> GetØvelserByNavnAsync(string navn)
        {
            return await _context.Øvelser
                .Where(o => o.ØvelseNavn.Contains(navn)) 
                .ToListAsync();
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
            _context.Entry(øvelse).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateØvelseAsync(Øvelse øvelse)
        {
            _context.Entry(øvelse).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // Soft delete methods
        public async Task<List<Øvelse>> GetAllØvelserIncludingDeletedAsync()
        {
            return await _context.Øvelser
                .IgnoreQueryFilters()  // Disable global filters for soft deletes
                .ToListAsync();
        }

        public async Task<Øvelse?> GetØvelseByIdIncludingDeletedAsync(Guid id)
        {
            return await _context.Øvelser
                .IgnoreQueryFilters()  // Ignore soft delete filter
                .FirstOrDefaultAsync(x => x.ØvelseID == id);
        }

        public async Task<Øvelse?> UpdateØvelseIncludingDeletedAsync(Guid id, Øvelse øvelse)
        {
            var existing = await _context.Øvelser.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.ØvelseID == id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(øvelse);  // Set updated values
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
