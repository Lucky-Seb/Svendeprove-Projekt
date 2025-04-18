using TaekwondoApp.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IQuizService
    {
        #region CRUD Operations

        Task<Result<IEnumerable<QuizDTO>>> GetAllQuizzesAsync(); // Get all quizzes
        Task<Result<QuizDTO>> GetQuizByIdAsync(Guid quizId); // Get a quiz by its ID
        Task<Result<QuizDTO>> CreateQuizAsync(QuizDTO quizDto); // Create a new quiz
        Task<Result<QuizDTO>> UpdateQuizAsync(Guid quizId, QuizDTO quizDto); // Update an existing quiz
        Task<Result<bool>> DeleteQuizAsync(Guid quizId); // Soft delete a quiz
        Task<Result<bool>> RestoreQuizAsync(Guid quizId, QuizDTO quizDto); // Restore a soft-deleted quiz

        #endregion

        #region Get Operations

        Task<Result<IEnumerable<QuizDTO>>> GetAllQuizzesByBrugerIdAsync(Guid brugerId); // Get quizzes by user
        Task<Result<IEnumerable<QuizDTO>>> GetAllQuizzesByKlubIdAsync(Guid klubId); // Get quizzes by club
        Task<Result<IEnumerable<QuizDTO>>> GetAllQuizzesIncludingDeletedAsync(); // Get all quizzes including deleted
        Task<Result<QuizDTO>> GetQuizByIdIncludingDeletedAsync(Guid quizId); // Get quiz by ID, including deleted quizzes
        Task<Result<IEnumerable<QuizDTO>>> GetAllQuizzesByPensumIdAsync(Guid pensumId); // Get quizzes by pensum
        
        #endregion
    }
}