using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
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

        public async Task<List<Quiz>> GetAllAsync()
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

        public async Task<Quiz?> GetByIdAsync(int id)
        {
            return await _context.Quizzer
            .Include(q => q.Spørgsmåls)
                .ThenInclude(s => s.Teknik)
            .Include(q => q.Spørgsmåls)
                .ThenInclude(s => s.Teori)
            .Include(q => q.Spørgsmåls)
                .ThenInclude(s => s.Øvelse)
            .FirstOrDefaultAsync(q => q.QuizID == id);
        }

        public async Task<Quiz> CreateAsync(Quiz quiz)
        {
            _context.Quizzer.Add(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task<bool> UpdateAsync(Quiz quiz)
        {
            _context.Entry(quiz).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var quiz = await _context.Quizzer.FindAsync(id);
            if (quiz == null) return false;

            _context.Quizzer.Remove(quiz);
            await _context.SaveChangesAsync();
            return true;
        }
        // Get all quizzes for a specific bruger (user)
        public async Task<List<Quiz>> GetAllByBrugerAsync(int brugerId)
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

        // Get all quizzes for a specific klub (club)
        public async Task<List<Quiz>> GetAllByKlubAsync(int klubId)
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

        // Get all quizzes for a specific pensum (curriculum)
        public async Task<List<Quiz>> GetAllByPensumAsync(int pensumId)
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
