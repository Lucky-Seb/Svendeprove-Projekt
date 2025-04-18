using TaekwondoApp.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IØvelseService
    {
        // CRUD Operations
        Task<Result<IEnumerable<ØvelseDTO>>> GetAllØvelserAsync();
        Task<Result<ØvelseDTO>> GetØvelseByIdAsync(Guid id);
        Task<Result<ØvelseDTO>> CreateØvelseAsync(ØvelseDTO øvelseDto);
        Task<Result<bool>> UpdateØvelseAsync(Guid id, ØvelseDTO øvelseDto);
        Task<Result<bool>> DeleteØvelseAsync(Guid id);

        // Additional Methods based on Filters
        Task<Result<IEnumerable<ØvelseDTO>>> GetØvelserBySværhedAsync(string sværhed);
        Task<Result<IEnumerable<ØvelseDTO>>> GetØvelserByBrugerAsync(Guid brugerId);
        Task<Result<IEnumerable<ØvelseDTO>>> GetØvelserByKlubAsync(Guid klubId);
        Task<Result<IEnumerable<ØvelseDTO>>> GetØvelserByNavnAsync(string navn);
    }
}
