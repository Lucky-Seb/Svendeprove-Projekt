using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class OrdbogRepository : IOrdbogRepository
    {
        private readonly ApiDbContext _context;

        public OrdbogRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ordbog>> GetAllOrdbogAsync()
        {
            return await _context.Ordboger.ToListAsync();  // No need to filter out IsDeleted
        }

        public async Task<Ordbog?> GetOrdbogByIdAsync(Guid ordbogId)
        {
            return await _context.Ordboger.FindAsync(ordbogId);  // No need to filter out IsDeleted
        }

        public async Task<Ordbog> CreateOrdbogAsync(Ordbog ordbog)
        {
            _context.Ordboger.Add(ordbog);
            await _context.SaveChangesAsync();
            return ordbog;
        }

        public async Task<bool> UpdateOrdbogAsync(Ordbog ordbog)
        {
            _context.Entry(ordbog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        // Repository UpdateOrdbog (simplified)
        public async Task<Ordbog?> UpdateOrdbogIncludingDeletedAsync(Guid id, Ordbog ordbog)
        {
            var existing = await _context.Ordboger.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.OrdbogId == id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(ordbog);  // Set updated values
            await _context.SaveChangesAsync();
            return existing;
        }
        // Soft delete Ordbog (update IsDeleted flag)
        public async Task<bool> DeleteOrdbogAsync(Guid ordbogId)
        {
            var ordbog = await _context.Ordboger.FindAsync(ordbogId);
            if (ordbog == null)
                return false;

            ordbog.IsDeleted = true;  // Mark as deleted
            _context.Entry(ordbog).State = EntityState.Modified;  // Mark entity as modified
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Ordbog?> GetOrdbogByDanskOrdAsync(string danskOrd)
        {
            return await _context.Ordboger
                .Where(o => o.DanskOrd.Equals(danskOrd, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync();  // No need to filter out IsDeleted
        }

        public async Task<Ordbog?> GetOrdbogByKoranskOrdAsync(string koranOrd)
        {
            return await _context.Ordboger
                .Where(o => o.KoranskOrd.Equals(koranOrd, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync();  // No need to filter out IsDeleted
        }
        public async Task<List<Ordbog>> GetAllOrdbogIncludingDeletedAsync()
        {
            return await _context.Ordboger
                .IgnoreQueryFilters()  // This disables the global soft delete filter
                .ToListAsync();
        }
        public async Task<Ordbog?> GetOrdbogByIdIncludingDeletedAsync(Guid id)
        {
            return await _context.Ordboger
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.OrdbogId == id);
        }

        public async Task<bool> UpdateAsync(Ordbog ordbog)
        {
            _context.Ordboger.Update(ordbog);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
