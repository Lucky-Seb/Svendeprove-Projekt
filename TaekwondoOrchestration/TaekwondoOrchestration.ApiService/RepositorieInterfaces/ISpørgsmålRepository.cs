using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface ISpørgsmålRepository
    {
        Task<List<Spørgsmål>> GetAllAsync();
        Task<Spørgsmål?> GetByIdAsync(int id);
        Task<Spørgsmål> CreateAsync(Spørgsmål spørgsmål);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Spørgsmål spørgsmål);
        Task<IEnumerable<Spørgsmål>> GetByQuizIdAsync(int quizId);
    }
}
