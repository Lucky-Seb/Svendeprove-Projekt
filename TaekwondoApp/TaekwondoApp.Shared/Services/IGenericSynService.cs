using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Model;

namespace TaekwondoApp.Shared.Services
{
    public interface IGenericSyncService
    {
        Task SyncDataFromServerAsync<T>(Func<Task<List<T>>> getServerData, Func<T, Task<T>> getLocalData, Func<T, Task> saveLocalData, Func<T, Task> updateLocalData) where T : SyncableEntity;
        Task SyncLocalChangesToServerAsync<T>(Func<Task<List<T>>> getUnsyncedEntries, Func<T, Task> postToServer, Func<T, Task> markAsSynced) where T : SyncableEntity;
    }
}
