using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class BrugerQuizService
    {
        private readonly IBrugerQuizRepository _brugerQuizRepository;

        public BrugerQuizService(IBrugerQuizRepository brugerQuizRepository)
        {
            _brugerQuizRepository = brugerQuizRepository;
        }

        public async Task<List<BrugerQuizDTO>> GetAllBrugerQuizzesAsync()
        {
            var brugerQuizzes = await _brugerQuizRepository.GetAllBrugerQuizzesAsync();
            return brugerQuizzes.Select(brugerQuiz => new BrugerQuizDTO
            {
                BrugerID = brugerQuiz.BrugerID,
                QuizID = brugerQuiz.QuizID
            }).ToList();
        }

        public async Task<BrugerQuizDTO?> GetBrugerQuizByIdAsync(Guid brugerId, Guid quizId)
        {
            var brugerQuiz = await _brugerQuizRepository.GetBrugerQuizByIdAsync(brugerId, quizId);
            if (brugerQuiz == null)
                return null;

            return new BrugerQuizDTO
            {
                BrugerID = brugerQuiz.BrugerID,
                QuizID = brugerQuiz.QuizID
            };
        }
        public async Task<BrugerQuizDTO?> CreateBrugerQuizAsync(BrugerQuizDTO brugerQuizDto)
        {
            // Check if the DTO is null
            if (brugerQuizDto == null) return null;

            // Validate required fields
            //if (brugerQuizDto.BrugerID <= 0) return null;  // BrugerID must be a positive integer
            //if (brugerQuizDto.QuizID <= 0) return null;    // QuizID must be a positive integer

            // Create new BrugerQuiz entity
            var newBrugerQuiz = new BrugerQuiz
            {
                BrugerID = brugerQuizDto.BrugerID,
                QuizID = brugerQuizDto.QuizID
            };

            // Save the new BrugerQuiz entity to the repository
            var createdBrugerQuiz = await _brugerQuizRepository.CreateBrugerQuizAsync(newBrugerQuiz);
            if (createdBrugerQuiz == null) return null;  // Return null if creation fails

            // Return the newly created BrugerQuizDTO
            return new BrugerQuizDTO
            {
                BrugerID = createdBrugerQuiz.BrugerID,
                QuizID = createdBrugerQuiz.QuizID
            };
        }


        public async Task<bool> DeleteBrugerQuizAsync(Guid brugerId, Guid quizId)
        {
            return await _brugerQuizRepository.DeleteBrugerQuizAsync(brugerId, quizId);
        }
    }
}
