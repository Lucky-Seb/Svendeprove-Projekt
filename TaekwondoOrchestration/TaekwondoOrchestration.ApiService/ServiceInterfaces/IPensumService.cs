using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IPensumService
    {
        #region CRUD Operations

        Task<Result<IEnumerable<PensumDTO>>> GetAllPensumAsync();

        Task<Result<PensumDTO>> GetPensumByIdAsync(Guid id);

        Task<Result<PensumDTO>> CreatePensumAsync(PensumDTO pensumDto);

        Task<Result<bool>> UpdatePensumAsync(Guid id, PensumDTO pensumDto);

        Task<Result<bool>> DeletePensumAsync(Guid id);

        Task<Result<bool>> RestorePensumAsync(Guid id, PensumDTO dto);

        #endregion

        #region Search Operations

        Task<Result<IEnumerable<PensumDTO>>> GetPensumByGradAsync(string grad);

        #endregion
    }
}
