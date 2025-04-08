using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class PensumRepository : IPensumRepository
    {
        private readonly ApiDbContext _context;

        public PensumRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pensum>> GetAllAsync()
        {
            return await _context.Pensum.ToListAsync();
        }

        public async Task<Pensum?> GetByIdAsync(int id)
        {
            return await _context.Pensum
                .Include(p => p.Teknikker)   // Eagerly load the Teknik related to Pensum
                .Include(p => p.Teorier)    // Eagerly load the Teori related to Pensum
                .FirstOrDefaultAsync(p => p.PensumID == id);
        }

        public async Task<Pensum> CreateAsync(Pensum pensum)
        {
            _context.Pensum.Add(pensum);
            await _context.SaveChangesAsync();
            return pensum;
        }

        public async Task<bool> UpdateAsync(Pensum pensum)
        {
            _context.Entry(pensum).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pensum = await _context.Pensum.FindAsync(id);
            if (pensum == null) return false;

            _context.Pensum.Remove(pensum);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
