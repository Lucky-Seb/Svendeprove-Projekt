using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface ITeknikService
    {
        #region CRUD Operations

        // Get All Teknikker
        Task<Result<IEnumerable<TeknikDTO>>> GetAllTekniksAsync();

        // Get Teknik by ID
        Task<Result<TeknikDTO>> GetTeknikByIdAsync(Guid id);

        // Create New Teknik
        Task<Result<TeknikDTO>> CreateTeknikAsync(TeknikDTO teknikDto);

        // Update Existing Teknik
        Task<Result<TeknikDTO>> UpdateTeknikAsync(Guid id, TeknikDTO teknikDto);

        // Soft Delete Teknik
        Task<Result<bool>> DeleteTeknikAsync(Guid id);

        // Restore Teknik from Soft-Delete
        Task<Result<bool>> RestoreTeknikAsync(Guid id, TeknikDTO teknikDto);

        #endregion

        #region Get Operations

        // Get Teknikker by Pensum ID
        Task<Result<IEnumerable<TeknikDTO>>> GetAllTeknikByPensumAsync(Guid pensumId);

        // Get Teknik by TeknikNavn
        Task<Result<TeknikDTO>> GetTeknikByTeknikNavnAsync(string teknikNavn);
        // New methods for fetching Tekniks including deleted ones
        Task<Result<IEnumerable<TeknikDTO>>> GetAllTeknikIncludingDeletedAsync();
        Task<Result<TeknikDTO>> GetTeknikByIdIncludingDeletedAsync(Guid id);


        #endregion
    }
}
