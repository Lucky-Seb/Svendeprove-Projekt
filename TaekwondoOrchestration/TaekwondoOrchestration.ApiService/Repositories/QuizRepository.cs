using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ApiDbContext _context;

        public QuizRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Quiz>> GetAllQuizzesAsync()
        {
            return await _context.Quizzer
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teknik)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teori)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Øvelse)
                .ToListAsync();
        }

        public async Task<Quiz?> GetQuizByIdAsync(Guid quizID)
        {
            return await _context.Quizzer
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teknik)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teori)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Øvelse)
                .FirstOrDefaultAsync(q => q.QuizID == quizID);
        }

        public async Task<Quiz?> GetQuizByIdIncludingDeletedAsync(Guid quizID)
        {
            return await _context.Quizzer
                .IgnoreQueryFilters()  // Ignore soft delete filter
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teknik)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teori)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Øvelse)
                .FirstOrDefaultAsync(q => q.QuizID == quizID);
        }

        public async Task<List<Quiz>> GetAllQuizzesIncludingDeletedAsync()
        {
            return await _context.Quizzer
                .IgnoreQueryFilters()  // Ignore soft delete filter
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teknik)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teori)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Øvelse)
                .ToListAsync();
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz)
        {
            _context.Quizzer.Add(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task<bool> UpdateQuizAsync(Quiz quiz)
        {
            _context.Entry(quiz).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteQuizAsync(Guid quizID)
        {
            var quiz = await _context.Quizzer.FindAsync(quizID);
            if (quiz == null) return false;

            _context.Quizzer.Remove(quiz);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Quiz>> GetAllQuizzesByBrugerAsync(Guid brugerId)
        {
            return await _context.Quizzer
                .Where(q => q.BrugerQuizzer.Any(bq => bq.BrugerID == brugerId))
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teknik)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teori)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Øvelse)
                .ToListAsync();
        }

        public async Task<List<Quiz>> GetAllQuizzesByKlubAsync(Guid klubId)
        {
            return await _context.Quizzer
                .Where(q => q.KlubQuizzer.Any(kq => kq.KlubID == klubId))
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teknik)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teori)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Øvelse)
                .ToListAsync();
        }

        public async Task<List<Quiz>> GetAllQuizzesByPensumAsync(Guid pensumId)
        {
            return await _context.Quizzer
                .Where(q => q.PensumID == pensumId)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teknik)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Teori)
                .Include(q => q.Spørgsmåls)
                    .ThenInclude(s => s.Øvelse)
                .ToListAsync();
        }
    }
}
