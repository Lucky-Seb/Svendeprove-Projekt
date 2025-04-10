using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface ISpørgsmålRepository
    {
        Task<List<Spørgsmål>> GetAllAsync();
        Task<Spørgsmål?> GetByIdAsync(Guid spørgsmålId);
        Task<Spørgsmål> CreateAsync(Spørgsmål spørgsmål);
        Task<bool> DeleteAsync(Guid spørgsmålId);
        Task<bool> UpdateAsync(Spørgsmål spørgsmål);
        Task<IEnumerable<Spørgsmål>> GetByQuizIdAsync(Guid quizId);
    }
}
