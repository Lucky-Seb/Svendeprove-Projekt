using AutoMapper;
using System.Net.Http.Json;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.ServiceInterfaces;

namespace TaekwondoApp.Shared.Services
{
    public class OrdbogSyncService : IOrdbogSyncService
    {
        private readonly ISQLiteService _sqliteService;
        private readonly HttpClient _httpClient;
        private readonly IGenericSyncService _syncService;
        private readonly IMapper _mapper;

        public OrdbogSyncService(IGenericSyncService syncService, ISQLiteService sqliteService, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _syncService = syncService;
            _sqliteService = sqliteService;
            _httpClient = httpClientFactory.CreateClient();
            _mapper = mapper;
        }

        public async Task SyncDataAsync()
        {
            try
            {
                // 1. Pull latest data from server
                var serverData = await _httpClient.GetFromJsonAsync<List<OrdbogDTO>>("https://localhost:7478/api/ordbog/including-deleted");
                Console.WriteLine($"Fetched {serverData.Count} entries from the server.");

                foreach (var entryDTO in serverData)
                {
                    try
                    {
                        var localEntry = await _sqliteService.GetEntryByIdAsync(entryDTO.OrdbogId);

                        if (localEntry == null)
                        {
                            // If entry doesn't exist locally, map and add it
                            var newEntry = _mapper.Map<Ordbog>(entryDTO);
                            await _sqliteService.AddEntryAsync(newEntry);
                            await _sqliteService.MarkAsSyncedAsync(newEntry.OrdbogId);
                        }
                        else if (entryDTO.ETag != localEntry.ETag)
                        {
                            // If ETag differs, check which version is newer and update accordingly
                            if (entryDTO.LastModified > localEntry.LastModified)
                            {
                                
                                var updatedEntry = _mapper.Map<Ordbog>(entryDTO);
                                await _sqliteService.UpdateEntryAsync(updatedEntry);
                                await _sqliteService.MarkAsSyncedAsync(entryDTO.OrdbogId);
                            }
                            else if (entryDTO.LastModified < localEntry.LastModified)
                            {
                                var updatedEntry = _mapper.Map<OrdbogDTO>(localEntry);
                                await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/including-deleted/{updatedEntry.OrdbogId}", updatedEntry);
                                await _sqliteService.MarkAsSyncedAsync(entryDTO.OrdbogId);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing server entry {entryDTO.OrdbogId}: {ex.Message}");
                    }
                }

                // 2. Handle deletions and restorations
                var deletedEntries = await _sqliteService.GetLocallyDeletedEntriesAsync();

                foreach (var deleted in deletedEntries)
                {
                    try
                    {
                        // Handle soft delete (if the entry is marked as deleted in the local database)
                        if (deleted.IsDeleted)
                        {
                            // Handle soft deletion on the server
                            var deleteResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/including-deleted/{deleted.OrdbogId}", deleted);

                            if (deleteResponse.IsSuccessStatusCode)
                            {
                                Console.WriteLine($"Deleted {deleted.OrdbogId} on server.");
                                await _sqliteService.MarkAsSyncedAsync(deleted.OrdbogId); // Mark as synced after successful deletion
                            }
                            else
                            {
                                Console.WriteLine($"Failed to delete {deleted.OrdbogId} on server: {deleteResponse.StatusCode}");
                            }
                        }
                        else
                        {
                            // Handle restore action for soft-deleted entries
                            var restoreResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/including-deleted/{deleted.OrdbogId}", deleted);

                            if (restoreResponse.IsSuccessStatusCode)
                            {
                                Console.WriteLine($"Restored {deleted.OrdbogId} on server.");
                                await _sqliteService.MarkAsSyncedAsync(deleted.OrdbogId); // Mark as restored after successful restore
                            }
                            else
                            {
                                Console.WriteLine($"Failed to restore {deleted.OrdbogId} on server: {restoreResponse.StatusCode}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error syncing deletion/restoration for {deleted.OrdbogId}: {ex.Message}");
                    }
                }

                // 3. Push local additions/updates
                var unsyncedEntries = await _sqliteService.GetUnsyncedEntriesAsync();

                foreach (var entry in unsyncedEntries)
                {
                    try
                    {
                        // Skip deleted entries (don't attempt to create or update them)
                        if (entry.IsDeleted)
                        {
                            // Handle deletion or restoration logic for deleted entries
                            var response = await _httpClient.GetAsync($"https://localhost:7478/api/ordbog/including-deleted/{entry.OrdbogId}");

                            if (response.IsSuccessStatusCode)
                            {
                                // If the entity exists on the server, handle soft delete
                                var existing = await response.Content.ReadFromJsonAsync<OrdbogDTO>();

                                // Only mark as deleted on the server if it's not already deleted there
                                if (!existing.IsDeleted)
                                {
                                    // Send soft delete to the server
                                    var deleteResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/including-deleted/{entry.OrdbogId}", entry);

                                    if (deleteResponse.IsSuccessStatusCode)
                                    {
                                        Console.WriteLine($"Successfully marked entry {entry.OrdbogId} as deleted on server.");
                                        await _sqliteService.MarkAsSyncedAsync(entry.OrdbogId); // Mark as synced after successful deletion
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Failed to mark entry {entry.OrdbogId} as deleted on server.");
                                    }
                                }
                            }
                            else
                            {
                                // If the entry doesn't exist on the server, no need to create it as it is marked for deletion locally
                                Console.WriteLine($"Entry {entry.OrdbogId} not found on server (likely deleted), skipping creation.");
                            }
                        }
                        else
                        {
                            // Only sync non-deleted entries (normal creation or update)
                            var response = await _httpClient.GetAsync($"https://localhost:7478/api/ordbog/including-deleted/{entry.OrdbogId}");

                            if (response.IsSuccessStatusCode)
                            {
                                // Entry exists on the server, update it if necessary
                                var existing = await response.Content.ReadFromJsonAsync<OrdbogDTO>();

                                // Handle conflicts: ETag mismatch and local modification timestamp is later than the server's timestamp
                                if (entry.ETag != existing.ETag && entry.LastModified > existing.LastModified)
                                {
                                    var dto = _mapper.Map<OrdbogDTO>(entry);

                                    // Update the existing entry on the server
                                    var updateResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/including-deleted/{entry.OrdbogId}", dto);

                                    if (updateResponse.IsSuccessStatusCode)
                                    {
                                        // Mark as synced after successful update
                                        await _sqliteService.MarkAsSyncedAsync(entry.OrdbogId);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Failed to update entry {entry.OrdbogId} on the server.");
                                    }
                                }
                            }
                            else
                            {
                                // Entry doesn't exist on the server — create new entry on the server
                                var dto = _mapper.Map<OrdbogDTO>(entry);

                                // Post the new entry to the server
                                var createResponse = await _httpClient.PostAsJsonAsync("https://localhost:7478/api/ordbog", dto);

                                if (createResponse.IsSuccessStatusCode)
                                {
                                    // Mark as synced after successful creation
                                    await _sqliteService.MarkAsSyncedAsync(entry.OrdbogId);
                                }
                                else
                                {
                                    Console.WriteLine($"Failed to create entry {entry.OrdbogId} on the server.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error syncing entry {entry.OrdbogId}: {ex.Message}");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during sync process: {ex.Message}");
            }
        }
    }
}
