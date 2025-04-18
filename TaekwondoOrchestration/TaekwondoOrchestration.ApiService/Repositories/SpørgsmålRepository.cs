using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class SpørgsmålRepository : ISpørgsmålRepository
    {
        private readonly ApiDbContext _context;

        public SpørgsmålRepository(ApiDbContext context)
        {
            _context = context;
        }

        // GET all Spørgsmål with related data
        public async Task<List<Spørgsmål>> GetAllAsync()
        {
            return await _context.Spørgsmål
                .Include(s => s.Teknik)
                .Include(s => s.Teori)
                .Include(s => s.Øvelse)
                .ToListAsync();
        }

        // GET a Spørgsmål by ID with related data
        public async Task<Spørgsmål?> GetByIdAsync(Guid spørgsmålId)
        {
            return await _context.Spørgsmål
                .Include(s => s.Teknik)
                .Include(s => s.Teori)
                .Include(s => s.Øvelse)
                .FirstOrDefaultAsync(s => s.SpørgsmålID == spørgsmålId);
        }

        // GET a Spørgsmål by ID including deleted records (for soft deletes)
        public async Task<Spørgsmål?> GetByIdIncludingDeletedAsync(Guid spørgsmålId)
        {
            return await _context.Spørgsmål
                .IgnoreQueryFilters()  // Ignores the soft delete filter
                .Include(s => s.Teknik)
                .Include(s => s.Teori)
                .Include(s => s.Øvelse)
                .FirstOrDefaultAsync(s => s.SpørgsmålID == spørgsmålId);
        }

        // GET all Spørgsmål including deleted records (for soft deletes)
        public async Task<List<Spørgsmål>> GetAllIncludingDeletedAsync()
        {
            return await _context.Spørgsmål
                .IgnoreQueryFilters()  // Ignores the soft delete filter
                .Include(s => s.Teknik)
                .Include(s => s.Teori)
                .Include(s => s.Øvelse)
                .ToListAsync();
        }

        // CREATE a new Spørgsmål
        public async Task<Spørgsmål> CreateAsync(Spørgsmål spørgsmål)
        {
            _context.Spørgsmål.Add(spørgsmål);
            await _context.SaveChangesAsync();
            return spørgsmål;
        }

        // UPDATE an existing Spørgsmål
        public async Task<bool> UpdateAsync(Spørgsmål spørgsmål)
        {
            _context.Entry(spørgsmål).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE a Spørgsmål
        public async Task<bool> DeleteAsync(Guid spørgsmålId)
        {
            var spørgsmål = await _context.Spørgsmål.FindAsync(spørgsmålId);
            if (spørgsmål == null)
                return false;

            _context.Spørgsmål.Remove(spørgsmål);
            await _context.SaveChangesAsync();
            return true;
        }

        // GET Spørgsmål by QuizId (with related data)
        public async Task<IEnumerable<Spørgsmål>> GetByQuizIdAsync(Guid quizId)
        {
            return await _context.Spørgsmål
                .Where(s => s.QuizID == quizId) // Filter by QuizID
                .Include(s => s.Teknik)
                .Include(s => s.Teori)
                .Include(s => s.Øvelse)
                .ToListAsync();
        }
    }
}
