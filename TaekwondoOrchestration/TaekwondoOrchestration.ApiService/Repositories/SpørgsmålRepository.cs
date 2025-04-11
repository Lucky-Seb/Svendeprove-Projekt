using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class SpørgsmålRepository : ISpørgsmålRepository
    {
        private readonly ApiDbContext _context;

        public SpørgsmålRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Spørgsmål>> GetAllAsync()
        {
            return await _context.Spørgsmål.ToListAsync();
        }

        public async Task<Spørgsmål?> GetByIdAsync(Guid spørgsmålId)
        {
            return await _context.Spørgsmål.FindAsync(spørgsmålId);
        }

        public async Task<Spørgsmål> CreateAsync(Spørgsmål spørgsmål)
        {
            _context.Spørgsmål.Add(spørgsmål);
            await _context.SaveChangesAsync();
            return spørgsmål;
        }

        public async Task<bool> DeleteAsync(Guid spørgsmålId)
        {
            var spørgsmål = await _context.Spørgsmål.FindAsync(spørgsmålId);
            if (spørgsmål == null)
                return false;

            _context.Spørgsmål.Remove(spørgsmål);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Spørgsmål spørgsmål)
        {
            _context.Entry(spørgsmål).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Spørgsmål>> GetByQuizIdAsync(Guid quizId)
        {
            // Ensure that the context includes the necessary relationships (e.g., QuizID).
            return await _context.Spørgsmål
                                 .Where(s => s.QuizID == quizId) // Filter by QuizID
                                 .ToListAsync(); // Retrieve as a list asynchronously
        }
    }
}
