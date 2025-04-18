using TaekwondoApp.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface ISpørgsmålService
    {
        #region CRUD Operations

        // Get All Spørgsmål
        Task<Result<IEnumerable<SpørgsmålDTO>>> GetAllSpørgsmålAsync();

        // Get Spørgsmål by ID
        Task<Result<SpørgsmålDTO>> GetSpørgsmålByIdAsync(Guid id);

        // Create New Spørgsmål
        Task<Result<SpørgsmålDTO>> CreateSpørgsmålAsync(SpørgsmålDTO spørgsmålDto);

        // Update Existing Spørgsmål
        Task<Result<SpørgsmålDTO>> UpdateSpørgsmålAsync(Guid id, SpørgsmålDTO spørgsmålDto);

        // Soft Delete Spørgsmål
        Task<Result<bool>> DeleteSpørgsmålAsync(Guid id);

        // Restore Spørgsmål from Soft-Delete
        Task<Result<bool>> RestoreSpørgsmålAsync(Guid id, SpørgsmålDTO spørgsmålDto);

        #endregion

        #region Get Operations

        // Get Spørgsmål by QuizId
        Task<Result<IEnumerable<SpørgsmålDTO>>> GetSpørgsmålByQuizIdAsync(Guid quizId);

        // Get All Spørgsmål Including Deleted (for soft deletes)
        Task<Result<IEnumerable<SpørgsmålDTO>>> GetAllSpørgsmålIncludingDeletedAsync();

        // Get Spørgsmål by ID Including Deleted
        Task<Result<SpørgsmålDTO>> GetSpørgsmålByIdIncludingDeletedAsync(Guid id);

        #endregion
    }
}
