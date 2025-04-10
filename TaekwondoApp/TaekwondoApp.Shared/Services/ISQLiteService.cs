using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoApp.Shared.Services
{
    public interface ISQLiteService
    {
        Task<int> AddEntryAsync(OrdbogDTO entry);
        Task<OrdbogDTO[]> GetAllEntriesAsync();
        Task<int> DeleteEntryAsync(Guid OrdbogId); // Mark as deleted instead of actually deleting from DB
        Task<int> UpdateEntryAsync(OrdbogDTO entry);
        Task<OrdbogDTO> GetEntryByIdAsync(Guid OrdbogId);
        void InitializeDatabase();
        Task<int> UpdateEntryWithServerIdAsync(OrdbogDTO entry);

        // Get entries that need to be synced (Pending or Failed)
        Task<OrdbogDTO[]> GetUnsyncedEntriesAsync(); // instead of List<OrdbogDTO>

        // Mark an entry as synced after pushing to the server
        Task<int> MarkAsSyncedAsync(Guid OrdbogId);

        // Mark an entry as failed when an error occurs during sync
        Task MarkAsFailedAsync(Guid OrdbogId);

        // Mark an entry as deleted (logical deletion)
        Task<int> MarkAsDeletedAsync(Guid OrdbogId); // New method to mark entry as deleted
    }
}
