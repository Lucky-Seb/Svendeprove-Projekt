using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Helpers; // Import Result<T>

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IOrdbogService
    {
        // CRUD Operations
        Task<Result<IEnumerable<OrdbogDTO>>> GetAllOrdbogAsync();
        Task<Result<OrdbogDTO>> GetOrdbogByIdAsync(Guid id);
        Task<Result<OrdbogDTO>> CreateOrdbogAsync(OrdbogDTO ordbogDto);
        Task<Result<bool>> UpdateOrdbogAsync(Guid id, OrdbogDTO ordbogDto);
        Task<Result<OrdbogDTO>> UpdateOrdbogIncludingDeletedByIdAsync(Guid id, OrdbogDTO ordbogDto);
        Task<Result<bool>> DeleteOrdbogAsync(Guid id);
        Task<Result<bool>> RestoreOrdbogAsync(Guid id, OrdbogDTO dto);

        // Search Operations
        Task<Result<OrdbogDTO>> GetOrdbogByDanskOrdAsync(string danskOrd);
        Task<Result<OrdbogDTO>> GetOrdbogByKoranOrdAsync(string koranOrd);
        Task<Result<IEnumerable<OrdbogDTO>>> GetAllOrdbogIncludingDeletedAsync();
        Task<Result<OrdbogDTO>> GetOrdbogByIdIncludingDeletedAsync(Guid id);
    }
}
