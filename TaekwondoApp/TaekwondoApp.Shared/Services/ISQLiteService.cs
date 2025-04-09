using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoApp.Shared.Services
{
    public interface ISQLiteService
    {
        Task<int> AddEntryAsync(OrdbogDTO entry);
        Task<List<OrdbogDTO>> GetAllEntriesAsync();
        Task<int> DeleteEntryAsync(int id);
        Task<int> UpdateEntryAsync(OrdbogDTO entry);
        Task<OrdbogDTO> GetEntryByIdAsync(int id);
        void InitializeDatabase();

        // NEW: Only get entries that need to be synced
        Task<List<OrdbogDTO>> GetUnsyncedEntriesAsync();

        // NEW: Mark an entry as synced after pushing to server
        Task<int> MarkAsSyncedAsync(int id);
    }
}
