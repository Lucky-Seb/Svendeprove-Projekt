using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class TræningRepository : ITræningRepository
    {
        private readonly ApiDbContext _context;

        public TræningRepository(ApiDbContext context)
        {
            _context = context;
        }

        // GET all Træning records with related data
        public async Task<List<Træning>> GetAllAsync()
        {
            return await _context.Træninger
                .Include(t => t.ProgramPlan) // Example of related entity (you can add more includes as needed)
                .ToListAsync();
        }

        // GET a Træning by ID with related data
        public async Task<Træning?> GetByIdAsync(Guid træningId)
        {
            return await _context.Træninger
                .Include(t => t.ProgramPlan) // Example of related entity
                .FirstOrDefaultAsync(t => t.TræningID == træningId);
        }

        // GET a Træning by ID including deleted records (for soft deletes)
        public async Task<Træning?> GetByIdIncludingDeletedAsync(Guid træningId)
        {
            return await _context.Træninger
                .IgnoreQueryFilters() // Ignores the soft delete filter
                .Include(t => t.ProgramPlan) // Example of related entity
                .FirstOrDefaultAsync(t => t.TræningID == træningId);
        }

        // GET all Træning records including deleted ones (for soft deletes)
        public async Task<List<Træning>> GetAllIncludingDeletedAsync()
        {
            return await _context.Træninger
                .IgnoreQueryFilters() // Ignores the soft delete filter
                .Include(t => t.ProgramPlan) // Example of related entity
                .ToListAsync();
        }

        // CREATE a new Træning
        public async Task<Træning> CreateAsync(Træning træning)
        {
            _context.Træninger.Add(træning);
            await _context.SaveChangesAsync();
            return træning;
        }

        // UPDATE an existing Træning
        public async Task<bool> UpdateAsync(Træning træning)
        {
            var existingTræning = await _context.Træninger.FindAsync(træning.TræningID);
            if (existingTræning == null)
                return false;

            _context.Entry(existingTræning).CurrentValues.SetValues(træning);
            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE a Træning
        public async Task<bool> DeleteAsync(Guid træningId)
        {
            var træning = await _context.Træninger.FindAsync(træningId);
            if (træning == null)
                return false;

            _context.Træninger.Remove(træning);
            await _context.SaveChangesAsync();
            return true;
        }

        // GET all Træning records by ProgramId (with related data)
        public async Task<List<Træning>> GetByProgramIdAsync(Guid programId)
        {
            return await _context.Træninger
                .Where(t => t.ProgramID == programId) // Filter by ProgramID
                .Include(t => t.ProgramPlan) // Example related entity
                .ToListAsync();
        }
    }
}
