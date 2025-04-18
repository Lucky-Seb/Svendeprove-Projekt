using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface ITeoriService
    {
        #region CRUD Operations

        // Get all Teorier
        Task<Result<IEnumerable<TeoriDTO>>> GetAllTeoriAsync();

        // Get Teori by ID
        Task<Result<TeoriDTO>> GetTeoriByIdAsync(Guid id);

        // Create New Teori
        Task<Result<TeoriDTO>> CreateTeoriAsync(TeoriDTO teoriDto);

        // Update Existing Teori
        Task<Result<TeoriDTO>> UpdateTeoriAsync(Guid id, TeoriDTO teoriDto);

        // Soft Delete Teori
        Task<Result<bool>> DeleteTeoriAsync(Guid id);

        // Restore Teori from Soft-Delete
        Task<Result<bool>> RestoreTeoriAsync(Guid id, TeoriDTO teoriDto);

        #endregion

        #region Get Operations

        // Get Teori by Pensum ID
        Task<Result<IEnumerable<TeoriDTO>>> GetTeoriByPensumAsync(Guid pensumId);

        // Get Teori by TeoriNavn
        Task<Result<TeoriDTO>> GetTeoriByTeoriNavnAsync(string teoriNavn);

        // Get all Teori, including deleted ones
        Task<Result<IEnumerable<TeoriDTO>>> GetAllTeoriIncludingDeletedAsync();

        // Get Teori by ID, including deleted ones
        Task<Result<TeoriDTO>> GetTeoriByIdIncludingDeletedAsync(Guid id);

        #endregion
    }
}
