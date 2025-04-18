using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class PensumRepository : IPensumRepository
    {
        private readonly ApiDbContext _context;

        public PensumRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pensum>> GetAllPensumAsync()
        {
            return await _context.Pensum
                .ToListAsync();
        }

        public async Task<Pensum?> GetPensumByIdAsync(Guid pensumId)
        {
            return await _context.Pensum
                .FirstOrDefaultAsync(p => p.PensumID == pensumId);
        }

        public async Task<Pensum?> GetPensumByIdIncludingDeletedAsync(Guid pensumId)
        {
            return await _context.Pensum
                .IgnoreQueryFilters()  // Ignore global filters (soft delete) for this query
                .FirstOrDefaultAsync(p => p.PensumID == pensumId);
        }
        public async Task<Pensum?> GetPensumByGradAsync(string grad)
        {
            // Assuming PensumGrad is a column in the Pensum table
            return await _context.Pensum
                                 .Where(p => p.PensumGrad == grad)
                                 .FirstOrDefaultAsync();
        }

        public async Task<List<Pensum>> GetAllPensumIncludingDeletedAsync()
        {
            return await _context.Pensum
                .IgnoreQueryFilters()  // Ignore soft delete filter
                .ToListAsync();
        }

        public async Task<Pensum> CreatePensumAsync(Pensum pensum)
        {
            _context.Pensum.Add(pensum);
            await _context.SaveChangesAsync();
            return pensum;
        }

        public async Task<bool> UpdatePensumAsync(Pensum pensum)
        {
            _context.Entry(pensum).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePensumAsync(Guid pensumId)
        {
            var pensum = await _context.Pensum.FindAsync(pensumId);
            if (pensum == null)  // Check if already deleted
                return false;

            _context.Entry(pensum).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Pensum?> UpdatePensumIncludingDeletedAsync(Guid pensumId, Pensum pensum)
        {
            var existingPensum = await _context.Pensum
                .IgnoreQueryFilters()  // Ignore soft delete filter
                .FirstOrDefaultAsync(p => p.PensumID == pensumId);

            if (existingPensum == null) return null;

            _context.Entry(existingPensum).CurrentValues.SetValues(pensum);
            await _context.SaveChangesAsync();
            return existingPensum;
        }
    }
}
