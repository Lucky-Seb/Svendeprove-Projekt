using TaekwondoApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetAllQuizzesAsync(); // Get all quizzes
        Task<Quiz?> GetQuizByIdAsync(Guid quizID); // Get a quiz by its ID
        Task<Quiz?> GetQuizByIdIncludingDeletedAsync(Guid quizID); // Get quiz by ID, including deleted ones (soft delete)
        Task<List<Quiz>> GetAllQuizzesIncludingDeletedAsync(); // Get all quizzes, including soft deleted ones
        Task<Quiz> CreateQuizAsync(Quiz quiz); // Create a new quiz
        Task<bool> UpdateQuizAsync(Quiz quiz); // Update an existing quiz
        Task<bool> DeleteQuizAsync(Guid quizID); // Delete a quiz by its ID
        Task<List<Quiz>> GetAllQuizzesByBrugerAsync(Guid brugerId); // Get all quizzes for a specific bruger (user)
        Task<List<Quiz>> GetAllQuizzesByKlubAsync(Guid klubId); // Get all quizzes for a specific klub (club)
        Task<List<Quiz>> GetAllQuizzesByPensumAsync(Guid pensumId); // Get all quizzes for a specific pensum (curriculum)
        Task<Quiz?> GetQuizWithDetailsAsync(Guid quizId);

    }
}
