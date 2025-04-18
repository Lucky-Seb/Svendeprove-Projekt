using TaekwondoApp.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface ITræningService
    {
        #region CRUD Operations

        // Get All Træning records
        Task<Result<IEnumerable<TræningDTO>>> GetAllTræningAsync();

        // Get Træning by ID
        Task<Result<TræningDTO>> GetTræningByIdAsync(Guid id);

        // Create New Træning
        Task<Result<TræningDTO>> CreateTræningAsync(TræningDTO træningDto);

        // Update Existing Træning
        Task<Result<TræningDTO>> UpdateTræningAsync(Guid id, TræningDTO træningDto);

        // Soft Delete Træning
        Task<Result<bool>> DeleteTræningAsync(Guid id);

        // Restore Træning from Soft-Delete
        Task<Result<bool>> RestoreTræningAsync(Guid id, TræningDTO træningDto);

        #endregion

        #region Get Operations

        // Get all Træning by Program ID
        Task<Result<IEnumerable<TræningDTO>>> GetTræningByProgramIdAsync(Guid programId);

        // Get all Træning, including deleted ones
        Task<Result<IEnumerable<TræningDTO>>> GetAllTræningIncludingDeletedAsync();

        // Get Træning by ID, including deleted ones
        Task<Result<TræningDTO>> GetTræningByIdIncludingDeletedAsync(Guid id);

        #endregion
    }
}
