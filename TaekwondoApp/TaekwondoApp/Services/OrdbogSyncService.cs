using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoApp.Services
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
                                await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/{updatedEntry.OrdbogId}", updatedEntry);
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
                        // Check if the entry is marked as deleted (soft delete flag is set)
                        if (deleted.IsDeleted)
                        {
                            // Handle soft deletion on the server
                            var deleteResponse = await _httpClient.DeleteAsync($"https://localhost:7478/api/ordbog/{deleted.OrdbogId}");

                            if (deleteResponse.IsSuccessStatusCode)
                            {
                                Console.WriteLine($"Deleted {deleted.OrdbogId} on server.");
                                await _sqliteService.DeleteEntryAsync(deleted.OrdbogId); // Final delete locally
                            }
                            else
                            {
                                Console.WriteLine($"Failed to delete {deleted.OrdbogId} on server: {deleteResponse.StatusCode}");
                            }
                        }
                        else
                        {
                            // Handle restore action
                            var restoreResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/{deleted.OrdbogId}", deleted);
                            if (restoreResponse.IsSuccessStatusCode)
                            {
                                Console.WriteLine($"Restored {deleted.OrdbogId} on server.");
                                await _sqliteService.MarkAsSyncedAsync(deleted.OrdbogId); // Mark as restored
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

                foreach (var entry in unsyncedEntries.Where(e => !e.IsDeleted))
                {
                    try
                    {
                        var response = await _httpClient.GetAsync($"https://localhost:7478/api/ordbog/{entry.OrdbogId}");

                        if (response.IsSuccessStatusCode)
                        {
                            var existing = await response.Content.ReadFromJsonAsync<OrdbogDTO>();

                            if (entry.ETag != existing.ETag && entry.LastModified > existing.LastModified)
                            {
                                var dto = _mapper.Map<OrdbogDTO>(entry);
                                await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/{entry.OrdbogId}", dto);
                                await _sqliteService.MarkAsSyncedAsync(entry.OrdbogId);
                            }
                        }
                        else
                        {
                            // Not found on server — create new
                            var dto = _mapper.Map<OrdbogDTO>(entry);
                            await _httpClient.PostAsJsonAsync("https://localhost:7478/api/ordbog", dto);
                            await _sqliteService.MarkAsSyncedAsync(entry.OrdbogId);
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
