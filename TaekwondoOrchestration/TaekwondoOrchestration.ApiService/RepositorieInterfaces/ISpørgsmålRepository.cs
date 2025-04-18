using TaekwondoApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface ISpørgsmålRepository
    {
        // GET all Spørgsmål with related data
        Task<List<Spørgsmål>> GetAllAsync();

        // GET a Spørgsmål by ID with related data
        Task<Spørgsmål?> GetByIdAsync(Guid spørgsmålId);

        // GET a Spørgsmål by ID including deleted records (for soft deletes)
        Task<Spørgsmål?> GetByIdIncludingDeletedAsync(Guid spørgsmålId);

        // GET all Spørgsmål including deleted records (for soft deletes)
        Task<List<Spørgsmål>> GetAllIncludingDeletedAsync();

        // CREATE a new Spørgsmål
        Task<Spørgsmål> CreateAsync(Spørgsmål spørgsmål);

        // UPDATE an existing Spørgsmål
        Task<bool> UpdateAsync(Spørgsmål spørgsmål);

        // DELETE a Spørgsmål
        Task<bool> DeleteAsync(Guid spørgsmålId);

        // GET Spørgsmål by QuizId (with related data)
        Task<IEnumerable<Spørgsmål>> GetByQuizIdAsync(Guid quizId);
    }
}
