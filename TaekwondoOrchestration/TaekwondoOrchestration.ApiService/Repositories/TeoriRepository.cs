using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class TeoriRepository : ITeoriRepository
    {
        private readonly ApiDbContext _context;

        public TeoriRepository(ApiDbContext context)
        {
            _context = context;
        }

        // GET all Teori records with related data
        public async Task<List<Teori>> GetAllAsync()
        {
            return await _context.Teorier
                .Include(t => t.Pensum) // Example of related entity (you can add more includes as needed)
                .ToListAsync();
        }

        // GET a Teori by ID with related data
        public async Task<Teori?> GetByIdAsync(Guid teoriId)
        {
            return await _context.Teorier
                .Include(t => t.Pensum) // Example of related entity
                .FirstOrDefaultAsync(t => t.TeoriID == teoriId);
        }

        // GET a Teori by ID including deleted records (for soft deletes)
        public async Task<Teori?> GetByIdIncludingDeletedAsync(Guid teoriId)
        {
            return await _context.Teorier
                .IgnoreQueryFilters() // Ignores the soft delete filter
                .Include(t => t.Pensum) // Example of related entity
                .FirstOrDefaultAsync(t => t.TeoriID == teoriId);
        }

        // GET all Teori records including deleted ones (for soft deletes)
        public async Task<List<Teori>> GetAllIncludingDeletedAsync()
        {
            return await _context.Teorier
                .IgnoreQueryFilters() // Ignores the soft delete filter
                .Include(t => t.Pensum) // Example of related entity
                .ToListAsync();
        }

        // CREATE a new Teori
        public async Task<Teori> CreateAsync(Teori teori)
        {
            _context.Teorier.Add(teori);
            await _context.SaveChangesAsync();
            return teori;
        }

        // UPDATE an existing Teori
        public async Task<bool> UpdateAsync(Teori teori)
        {
            _context.Entry(teori).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE a Teori
        public async Task<bool> DeleteAsync(Guid teoriId)
        {
            var teori = await _context.Teorier.FindAsync(teoriId);
            if (teori == null)
                return false;

            _context.Teorier.Remove(teori);
            await _context.SaveChangesAsync();
            return true;
        }

        // GET all Teori by PensumId (with related data)
        public async Task<List<Teori>> GetByPensumIdAsync(Guid pensumId)
        {
            return await _context.Teorier
                .Where(t => t.PensumID == pensumId) // Filter by PensumID
                .Include(t => t.Pensum) // Example related entity
                .ToListAsync();
        }

        // GET Teori by TeoriNavn (for name search)
        public async Task<Teori?> GetByTeoriNavnAsync(string teoriNavn)
        {
            return await _context.Teorier
                .FirstOrDefaultAsync(t => t.TeoriNavn == teoriNavn);
        }

        // GET Teori by PensumId including deleted records (for soft deletes)
        public async Task<List<Teori>> GetByPensumIdIncludingDeletedAsync(Guid pensumId)
        {
            return await _context.Teorier
                .IgnoreQueryFilters() // Ignores the soft delete filter
                .Where(t => t.PensumID == pensumId) // Filter by PensumID
                .Include(t => t.Pensum) // Example related entity
                .ToListAsync();
        }
    }
}
