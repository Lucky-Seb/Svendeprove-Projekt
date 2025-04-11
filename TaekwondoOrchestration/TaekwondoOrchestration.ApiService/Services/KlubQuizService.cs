using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class KlubQuizService
    {
        private readonly IKlubQuizRepository _klubQuizRepository;

        public KlubQuizService(IKlubQuizRepository klubQuizRepository)
        {
            _klubQuizRepository = klubQuizRepository;
        }

        public async Task<List<KlubQuizDTO>> GetAllKlubQuizzerAsync()
        {
            var klubQuizzer = await _klubQuizRepository.GetAllKlubQuizzerAsync();
            return klubQuizzer.Select(k => new KlubQuizDTO
            {
                KlubID = k.KlubID,
                QuizID = k.QuizID
            }).ToList();
        }

        public async Task<KlubQuizDTO?> GetKlubQuizByIdAsync(Guid klubId, Guid quizId)
        {
            var klubQuiz = await _klubQuizRepository.GetKlubQuizByIdAsync(klubId, quizId);
            if (klubQuiz == null)
                return null;

            return new KlubQuizDTO
            {
                KlubID = klubQuiz.KlubID,
                QuizID = klubQuiz.QuizID
            };
        }

        public async Task<KlubQuizDTO?> CreateKlubQuizAsync(KlubQuizDTO klubQuizDto)
        {
            // Check if the DTO is null
            if (klubQuizDto == null) return null;

            // Validate required fields
            //if (klubQuizDto.KlubID <= 0) return null;  // KlubID must be a positive integer
            //if (klubQuizDto.QuizID <= 0) return null;  // QuizID must be a positive integer

            // Create new KlubQuiz entity
            var newKlubQuiz = new KlubQuiz
            {
                KlubID = klubQuizDto.KlubID,
                QuizID = klubQuizDto.QuizID
            };

            // Save the new KlubQuiz entity
            var createdKlubQuiz = await _klubQuizRepository.CreateKlubQuizAsync(newKlubQuiz);

            // Return the newly created KlubQuizDTO
            return new KlubQuizDTO
            {
                KlubID = createdKlubQuiz.KlubID,
                QuizID = createdKlubQuiz.QuizID
            };
        }


        public async Task<bool> DeleteKlubQuizAsync(Guid klubId, Guid quizId)
        {
            return await _klubQuizRepository.DeleteKlubQuizAsync(klubId, quizId);
        }
    }
}
