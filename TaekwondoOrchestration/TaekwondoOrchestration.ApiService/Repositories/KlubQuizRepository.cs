using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class KlubQuizRepository : IKlubQuizRepository
    {
        private readonly ApiDbContext _context;

        public KlubQuizRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<KlubQuiz>> GetAllKlubQuizzerAsync()
        {
            return await _context.KlubQuizzer.ToListAsync();
        }

        public async Task<KlubQuiz?> GetKlubQuizByIdAsync(int klubId, int quizId)
        {
            return await _context.KlubQuizzer
                .FirstOrDefaultAsync(k => k.KlubID == klubId && k.QuizID == quizId);
        }

        public async Task<KlubQuiz> CreateKlubQuizAsync(KlubQuiz klubQuiz)
        {
            _context.KlubQuizzer.Add(klubQuiz);
            await _context.SaveChangesAsync();
            return klubQuiz;
        }

        public async Task<bool> DeleteKlubQuizAsync(int klubId, int quizId)
        {
            var klubQuiz = await _context.KlubQuizzer
                .FirstOrDefaultAsync(k => k.KlubID == klubId && k.QuizID == quizId);
            if (klubQuiz == null)
                return false;

            _context.KlubQuizzer.Remove(klubQuiz);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
