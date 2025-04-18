using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class TeknikRepository : ITeknikRepository
    {
        private readonly ApiDbContext _context;

        public TeknikRepository(ApiDbContext context)
        {
            _context = context;
        }

        // GET all Teknikker with related data
        public async Task<List<Teknik>> GetAllAsync()
        {
            return await _context.Teknikker
                .Include(t => t.Pensum) // Example related entity (add more as needed)
                .ToListAsync();
        }

        // GET a Teknik by ID with related data
        public async Task<Teknik?> GetByIdAsync(Guid teknikId)
        {
            return await _context.Teknikker
                .Include(t => t.Pensum) // Example related entity
                .FirstOrDefaultAsync(t => t.TeknikID == teknikId);
        }

        // GET a Teknik by ID including deleted records (for soft deletes)
        public async Task<Teknik?> GetByIdIncludingDeletedAsync(Guid teknikId)
        {
            return await _context.Teknikker
                .IgnoreQueryFilters() // Ignores the soft delete filter
                .Include(t => t.Pensum) // Example related entity
                .FirstOrDefaultAsync(t => t.TeknikID == teknikId);
        }

        // GET all Teknikker including deleted records (for soft deletes)
        public async Task<List<Teknik>> GetAllIncludingDeletedAsync()
        {
            return await _context.Teknikker
                .IgnoreQueryFilters() // Ignores the soft delete filter
                .Include(t => t.Pensum) // Example related entity
                .ToListAsync();
        }

        // CREATE a new Teknik
        public async Task<Teknik> CreateAsync(Teknik teknik)
        {
            _context.Teknikker.Add(teknik);
            await _context.SaveChangesAsync();
            return teknik;
        }

        // UPDATE an existing Teknik
        public async Task<bool> UpdateAsync(Teknik teknik)
        {
            _context.Entry(teknik).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE a Teknik
        public async Task<bool> DeleteAsync(Guid teknikId)
        {
            var teknik = await _context.Teknikker.FindAsync(teknikId);
            if (teknik == null)
                return false;

            _context.Teknikker.Remove(teknik);
            await _context.SaveChangesAsync();
            return true;
        }

        // GET Teknikker by PensumId (with related data)
        public async Task<List<Teknik>> GetByPensumIdAsync(Guid pensumId)
        {
            return await _context.Teknikker
                .Where(t => t.PensumID == pensumId) // Filter by PensumID
                .Include(t => t.Pensum) // Example related entity
                .ToListAsync();
        }

        // GET Teknik by TeknikNavn (for example, name search)
        public async Task<Teknik?> GetByTeknikNavnAsync(string teknikNavn)
        {
            return await _context.Teknikker
                .FirstOrDefaultAsync(t => t.TeknikNavn == teknikNavn);
        }

        // GET Teknikker by PensumId including deleted records (for soft deletes)
        public async Task<List<Teknik>> GetByPensumIdIncludingDeletedAsync(Guid pensumId)
        {
            return await _context.Teknikker
                .IgnoreQueryFilters() // Ignores the soft delete filter
                .Where(t => t.PensumID == pensumId) // Filter by PensumID
                .Include(t => t.Pensum) // Example related entity
                .ToListAsync();
        }
    }
}
