using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.ServiceInterfaces;

namespace TaekwondoApp.Web.Services
{


    public class DummySQLiteService : ISQLiteService
    {
        public Task<int> AddEntryAsync(Ordbog entry) => Task.FromResult(0);

        public Task<Ordbog[]> GetAllEntriesAsync() => Task.FromResult(Array.Empty<Ordbog>());

        public Task<int> DeleteEntryAsync(Guid OrdbogId) => Task.FromResult(0);

        public Task<int> UpdateEntryAsync(Ordbog entry) => Task.FromResult(0);

        public Task<Ordbog> GetEntryByIdAsync(Guid OrdbogId) =>
            Task.FromResult<Ordbog>(null!);  // or return dummy entry

        public void InitializeDatabase() { }

        public Task<int> UpdateEntryWithServerIdAsync(Ordbog entry) => Task.FromResult(0);

        public Task<Ordbog[]> GetUnsyncedEntriesAsync() => Task.FromResult(Array.Empty<Ordbog>());

        public Task<int> MarkAsSyncedAsync(Guid OrdbogId) => Task.FromResult(0);

        public Task MarkAsFailedAsync(Guid OrdbogId) => Task.CompletedTask;

        public Task<int> MarkAsDeletedAsync(Guid OrdbogId) => Task.FromResult(0);

        public Task<List<Ordbog>> GetLocallyDeletedEntriesAsync() =>
            Task.FromResult(new List<Ordbog>());

        public Task MarkAsRestoredAsync(Guid OrdbogId) => Task.CompletedTask;
    }
}
