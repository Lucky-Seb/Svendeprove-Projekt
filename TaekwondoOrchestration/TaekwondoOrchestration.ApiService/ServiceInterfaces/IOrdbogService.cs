using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IOrdbogService
    {
        // CRUD
        Task<List<OrdbogDTO>> GetAllOrdbogAsync();
        Task<OrdbogDTO?> GetOrdbogByIdAsync(Guid id);
        Task<OrdbogDTO> CreateOrdbogAsync(OrdbogDTO ordbogDto);
        Task<bool> UpdateOrdbogAsync(Guid id, OrdbogDTO ordbogDto);
        Task<OrdbogDTO?> UpdateOrdbogIncludingDeletedByIdAsync(Guid id, OrdbogDTO ordbogDto);
        Task<bool> DeleteOrdbogAsync(Guid id);
        Task<bool> RestoreOrdbogAsync(Guid id, OrdbogDTO dto);

        // Search
        Task<OrdbogDTO?> GetOrdbogByDanskOrdAsync(string danskOrd);
        Task<OrdbogDTO?> GetOrdbogByKoranOrdAsync(string koranOrd);
        Task<IEnumerable<OrdbogDTO>> GetAllOrdbogIncludingDeletedAsync();
    }
}
