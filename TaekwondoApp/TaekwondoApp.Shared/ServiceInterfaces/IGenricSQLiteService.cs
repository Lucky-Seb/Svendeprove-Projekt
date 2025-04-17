using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;

namespace TaekwondoApp.Shared.ServiceInterfaces
{
    public interface IGenericSQLiteService<T> where T : SyncableEntity, new()
    {
        void InitializeDatabase();
        Task<T[]> GetAllEntriesAsync();
        Task<T> GetEntryByIdAsync(Guid entryId);
        Task<int> AddEntryAsync(T entry);
        Task<int> UpdateEntryAsync(T entry);
        Task<int> DeleteEntryAsync(Guid entryId);
        Task<int> MarkAsSyncedAsync(Guid entryId);
        Task<int> MarkAsDeletedAsync(Guid entryId);
        Task<int> MarkAsRestoredAsync(Guid entryId);
        Task<List<T>> GetLocallyDeletedEntriesAsync();
        Task<T[]> GetUnsyncedEntriesAsync();
    }
}
