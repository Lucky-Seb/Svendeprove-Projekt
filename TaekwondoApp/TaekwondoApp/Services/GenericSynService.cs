using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.Services;

namespace TaekwondoApp.Services
{
    public class GenericSyncService : IGenericSyncService
    {
        private readonly HttpClient _httpClient;
        private readonly ISQLiteService _sqliteService;

        public GenericSyncService(HttpClient httpClient, ISQLiteService sqliteService)
        {
            _httpClient = httpClient;
            _sqliteService = sqliteService;
        }

        public async Task SyncDataFromServerAsync<T>(
            Func<Task<List<T>>> getServerData,
            Func<T, Task<T>> getLocalData,
            Func<T, Task> saveLocalData,
            Func<T, Task> updateLocalData)
            where T : SyncableEntity
        {
            var serverEntries = await getServerData();

            foreach (var serverEntry in serverEntries)
            {
                var localEntry = await getLocalData(serverEntry);

                if (localEntry == null)
                {
                    // Entry doesn't exist locally, add it
                    await saveLocalData(serverEntry);
                }
                else
                {
                    // Check if ETag matches before considering other conditions
                    if (serverEntry.ETag != null && serverEntry.ETag == localEntry.ETag)
                    {
                        // If ETag matches, no data changes on the server, so skip the API call
                        continue;
                    }

                    // If the timestamps are equal but the actual data is different, it's still a conflict
                    if (serverEntry.LastModified == localEntry.LastModified)
                    {
                        // Compare the actual data fields (e.g., business logic fields)
                        if (!CompareData(serverEntry, localEntry))  // Implement this method to compare fields, if necessary
                        {
                            // Data is different, conflict detected
                            serverEntry.ConflictStatus = ConflictResolutionStatus.ManualResolve;
                        }
                        else
                        {
                            // If data is the same (even with equal timestamps), no conflict
                            serverEntry.ConflictStatus = ConflictResolutionStatus.NoConflict;
                        }
                    }
                    else if (serverEntry.LastModified > localEntry.LastModified)
                    {
                        // Server data is newer
                        await updateLocalData(serverEntry);
                        serverEntry.ConflictStatus = ConflictResolutionStatus.ServerWins;
                    }
                    else if (serverEntry.LastModified < localEntry.LastModified)
                    {
                        // Local data is newer
                        var response = await _httpClient.PostAsJsonAsync("https://localhost:7478/api/", localEntry);

                        if (response.IsSuccessStatusCode)
                        {
                            localEntry.ConflictStatus = ConflictResolutionStatus.LocalWins;
                        }
                        else
                        {
                            localEntry.ConflictStatus = ConflictResolutionStatus.ManualResolve;
                        }
                    }

                    // Update the sync status and versioning
                    localEntry.Status = SyncStatus.Synced;
                    localEntry.LastSyncedVersion++;

                    // Proceed to update the entry
                    await updateLocalData(localEntry);
                }
            }
        }
        public async Task SyncLocalChangesToServerAsync<T>(
            Func<Task<List<T>>> getUnsyncedEntries,
            Func<T, Task> postToServer,
            Func<T, Task> markAsSynced)
            where T : SyncableEntity
        {
            var unsyncedEntries = await getUnsyncedEntries();

            foreach (var entry in unsyncedEntries)
            {
                if (entry.IsDeleted)
                {
                    // Handle deleted entries (you could delete them from the server, or skip them)
                    continue;
                }

                // Check if ETag exists and compare it before making the API call
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7478/api/")
                {
                    Content = JsonContent.Create(entry)
                };

                // If the ETag is available, add it to the request headers to prevent overwriting data if the server's version differs
                if (!string.IsNullOrEmpty(entry.ETag))
                {
                    requestMessage.Headers.Add("If-Match", entry.ETag); // Using If-Match header with ETag for versioning
                }

                // Set the ModifiedBy field to track the user or device that is making the change
                entry.ModifiedBy = "System";  // Replace with actual user/device ID if available

                // Sending the request
                var response = await _httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    // Upon successful sync, mark as synced and update status
                    await markAsSynced(entry);

                    // Update sync status
                    entry.Status = SyncStatus.Synced;
                    entry.LastSyncedVersion++;  // Increment version on sync
                    entry.ConflictStatus = ConflictResolutionStatus.NoConflict; // Reset conflict status

                    // Optionally, log the changes to ChangeHistory
                    var changeRecord = new ChangeRecord
                    {
                        ChangedAt = DateTime.UtcNow,
                        ChangedBy = "System", // Track who made the change
                        ChangeDescription = "Sync successful"
                    };
                    entry.ChangeHistory.Add(changeRecord);
                }
                else
                {
                    // If sync fails, mark entry as failed or handle retries
                    entry.Status = SyncStatus.Failed;
                    entry.ConflictStatus = ConflictResolutionStatus.ManualResolve; // Flag for manual resolution

                    // Optionally, log the changes to ChangeHistory
                    var changeRecord = new ChangeRecord
                    {
                        ChangedAt = DateTime.UtcNow,
                        ChangedBy = "System", // Track who made the change
                        ChangeDescription = "Sync failed"
                    };
                    entry.ChangeHistory.Add(changeRecord);
                }
            }
        }

        private bool CompareData<T>(T serverEntry, T localEntry) where T : SyncableEntity
        {
            // Implement the actual data comparison logic here
            // For example, comparing relevant fields like:
            //   serverEntry.SomeField != localEntry.SomeField

            // Example: Compare specific fields
            if (serverEntry.ETag != localEntry.ETag)
            {
                return false;
            }
            // Add more comparisons for other fields as necessary
            return true;
        }

    }
}
