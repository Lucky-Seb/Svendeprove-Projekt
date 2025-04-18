using TaekwondoApp.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IKlubService
    {
        Task<Result<IEnumerable<KlubDTO>>> GetAllKlubberAsync();
        Task<Result<KlubDTO>> GetKlubByIdAsync(Guid id);
        Task<Result<KlubDTO>> GetKlubByNavnAsync(string klubNavn);
        Task<Result<KlubDTO>> CreateKlubAsync(KlubDTO klubDto);
        Task<Result<bool>> UpdateKlubAsync(Guid id, KlubDTO klubDto);
        Task<Result<bool>> DeleteKlubAsync(Guid id);
        Task<Result<IEnumerable<KlubDTO>>> GetKlubberByNavnAsync(string klubNavn);
    }
}
