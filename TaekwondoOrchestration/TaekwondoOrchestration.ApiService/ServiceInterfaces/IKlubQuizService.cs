using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IKlubQuizService
    {
        // Get all KlubQuizzer
        Task<Result<IEnumerable<KlubQuizDTO>>> GetAllKlubQuizzerAsync();

        // Get KlubQuiz by ID (KlubId, QuizId)
        Task<Result<KlubQuizDTO>> GetKlubQuizByIdAsync(Guid klubId, Guid quizId);

        // Create new KlubQuiz
        Task<Result<KlubQuizDTO>> CreateKlubQuizAsync(KlubQuizDTO klubQuizDto);

        // Delete KlubQuiz by ID (KlubId, QuizId)
        Task<Result<bool>> DeleteKlubQuizAsync(Guid klubId, Guid quizId);
    }
}
