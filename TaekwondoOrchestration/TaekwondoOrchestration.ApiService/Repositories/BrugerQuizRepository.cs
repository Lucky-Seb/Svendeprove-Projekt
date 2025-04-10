using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class BrugerQuizRepository : IBrugerQuizRepository
    {
        private readonly ApiDbContext _context;

        public BrugerQuizRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<BrugerQuiz>> GetAllBrugerQuizzesAsync()
        {
            return await _context.BrugerQuizzer.ToListAsync();
        }

        public async Task<BrugerQuiz?> GetBrugerQuizByIdAsync(Guid brugerId, Guid quizId)
        {
            return await _context.BrugerQuizzer
                .FirstOrDefaultAsync(bq => bq.BrugerID == brugerId && bq.QuizID == quizId);
        }

        public async Task<BrugerQuiz> CreateBrugerQuizAsync(BrugerQuiz brugerQuiz)
        {
            _context.BrugerQuizzer.Add(brugerQuiz);
            await _context.SaveChangesAsync();
            return brugerQuiz;
        }

        public async Task<bool> DeleteBrugerQuizAsync(Guid brugerId, Guid quizId)
        {
            var brugerQuiz = await _context.BrugerQuizzer
                .FirstOrDefaultAsync(bq => bq.BrugerID == brugerId && bq.QuizID == quizId);
            if (brugerQuiz == null)
                return false;

            _context.BrugerQuizzer.Remove(brugerQuiz);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
