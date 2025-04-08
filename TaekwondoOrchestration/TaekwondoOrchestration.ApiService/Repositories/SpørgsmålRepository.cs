using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
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

        public async Task<Spørgsmål?> GetByIdAsync(int id)
        {
            return await _context.Spørgsmål.FindAsync(id);
        }

        public async Task<Spørgsmål> CreateAsync(Spørgsmål spørgsmål)
        {
            _context.Spørgsmål.Add(spørgsmål);
            await _context.SaveChangesAsync();
            return spørgsmål;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var spørgsmål = await _context.Spørgsmål.FindAsync(id);
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
        public async Task<IEnumerable<Spørgsmål>> GetByQuizIdAsync(int quizId)
        {
            // Ensure that the context includes the necessary relationships (e.g., QuizID).
            return await _context.Spørgsmål
                                 .Where(s => s.QuizID == quizId) // Filter by QuizID
                                 .ToListAsync(); // Retrieve as a list asynchronously
        }
    }
}
