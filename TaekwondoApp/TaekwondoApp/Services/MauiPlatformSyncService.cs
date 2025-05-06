using AutoMapper;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.ServiceInterfaces;
using TaekwondoApp.Shared.Helper;

namespace TaekwondoApp
{
    public class MauiPlatformSyncService : IPlatformSyncService
    {
        private readonly ISQLiteService _sqliteService;
        private readonly HttpClient _httpClient;
        private readonly IGenericSyncService _syncService;
        private readonly IMapper _mapper;

        public MauiPlatformSyncService(IGenericSyncService syncService, ISQLiteService sqliteService, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _syncService = syncService;
            _sqliteService = sqliteService;
            _httpClient = httpClientFactory.CreateClient();
            _mapper = mapper;
        }

        public async Task SyncOrdbogAsync()
        {
            try
            {
                // 1. Pull latest data from server
                var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<List<OrdbogDTO>>>("https://localhost:7478/api/ordbog/including-deleted");

                if (apiResponse == null || !apiResponse.Success)
                {
                    Console.WriteLine($"Failed to fetch data: {apiResponse?.StatusCode ?? 0}, errors: {string.Join(", ", apiResponse?.Errors ?? new())}");
                    return;
                }

                var serverData = apiResponse.Data;
                Console.WriteLine($"Fetched {serverData.Count} entries from the server.");

                foreach (var entryDTO in serverData)
                {
                    try
                    {
                        var localEntry = await _sqliteService.GetEntryByIdAsync(entryDTO.OrdbogId);

                        if (localEntry == null)
                        {
                            var newEntry = _mapper.Map<Ordbog>(entryDTO);
                            await _sqliteService.AddEntryAsync(newEntry);
                            await _sqliteService.MarkAsSyncedAsync(newEntry.OrdbogId);
                        }
                        else if (entryDTO.ETag != localEntry.ETag)
                        {
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
                        var deleteResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/including-deleted/{deleted.OrdbogId}", deleted);

                        if (deleteResponse.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"{(deleted.IsDeleted ? "Deleted" : "Restored")} {deleted.OrdbogId} on server.");
                            await _sqliteService.MarkAsSyncedAsync(deleted.OrdbogId);
                        }
                        else
                        {
                            Console.WriteLine($"Failed to sync deletion/restoration for {deleted.OrdbogId}: {deleteResponse.StatusCode}");
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
                        // Check if the entry has been deleted
                        var response = await _httpClient.GetAsync($"https://localhost:7478/api/ordbog/including-deleted/{entry.OrdbogId}");

                        if (entry.IsDeleted)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var existing = await response.Content.ReadFromJsonAsync<ApiResponse<OrdbogDTO>>();
                                if (existing?.Data != null && !existing.Data.IsDeleted)
                                {
                                    var deleteResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/including-deleted/{entry.OrdbogId}", entry);

                                    if (deleteResponse.IsSuccessStatusCode)
                                    {
                                        Console.WriteLine($"Successfully marked entry {entry.OrdbogId} as deleted on server.");
                                        await _sqliteService.MarkAsSyncedAsync(entry.OrdbogId);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Failed to delete entry {entry.OrdbogId} on server.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Entry {entry.OrdbogId} not found on server, skipping creation.");
                            }
                        }
                        else
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var existing = await response.Content.ReadFromJsonAsync<ApiResponse<OrdbogDTO>>();
                                if (existing?.Data != null && entry.ETag != existing.Data.ETag && entry.LastModified > existing.Data.LastModified)
                                {
                                    var dto = _mapper.Map<OrdbogDTO>(entry);
                                    var updateResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/including-deleted/{entry.OrdbogId}", dto);

                                    if (updateResponse.IsSuccessStatusCode)
                                    {
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
                                var dto = _mapper.Map<OrdbogDTO>(entry);
                                var createResponse = await _httpClient.PostAsJsonAsync("https://localhost:7478/api/ordbog", dto);

                                if (createResponse.IsSuccessStatusCode)
                                {
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
