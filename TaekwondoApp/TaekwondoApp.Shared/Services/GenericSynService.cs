using AutoMapper;
using System.Net.Http.Json;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.ServiceInterfaces;

namespace TaekwondoApp.Shared.Services
{
    public class GenericSyncService<T, TDto> : IGenericSyncService<T, TDto>
        where T : SyncableEntity, new()
        where TDto : class
    {
        private readonly IGenericSQLiteService<T> _sqliteService;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public GenericSyncService(
            IGenericSQLiteService<T> sqliteService,
            IHttpClientFactory httpClientFactory,
            IMapper mapper)
        {
            _sqliteService = sqliteService;
            _httpClient = httpClientFactory.CreateClient();
            _mapper = mapper;
        }

        public async Task SyncDataAsync(string apiEndpoint)
        {
            try
            {
                // 1. Pull from server
                var serverData = await _httpClient.GetFromJsonAsync<List<TDto>>($"https://localhost:7478/api/{apiEndpoint}/including-deleted");
                if (serverData == null) return;

                foreach (var dto in serverData)
                {
                    var entity = _mapper.Map<T>(dto);
                    var local = await _sqliteService.GetEntryByIdAsync(entity.GetPrimaryKey());

                    if (local == null)
                    {
                        await _sqliteService.AddEntryAsync(entity);
                        await _sqliteService.MarkAsSyncedAsync(entity.GetPrimaryKey());
                    }
                    else if (entity.ETag != local.ETag)
                    {
                        if (entity.LastModified > local.LastModified)
                        {
                            await _sqliteService.UpdateEntryAsync(entity);
                            await _sqliteService.MarkAsSyncedAsync(entity.GetPrimaryKey());
                        }
                        else
                        {
                            var updatedDto = _mapper.Map<TDto>(local);
                            await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/{apiEndpoint}/including-deleted/{local.GetPrimaryKey()}", updatedDto);
                            await _sqliteService.MarkAsSyncedAsync(local.GetPrimaryKey());
                        }
                    }
                }

                // 2. Handle deletions
                var deletedLocals = await _sqliteService.GetLocallyDeletedEntriesAsync();
                foreach (var deleted in deletedLocals)
                {
                    var response = await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/{apiEndpoint}/including-deleted/{deleted.GetPrimaryKey()}", deleted);
                    if (response.IsSuccessStatusCode)
                    {
                        await _sqliteService.MarkAsSyncedAsync(deleted.GetPrimaryKey());
                    }
                }

                // 3. Push unsynced local changes
                var unsynced = await _sqliteService.GetUnsyncedEntriesAsync();
                foreach (var entry in unsynced)
                {
                    var dto = _mapper.Map<TDto>(entry);
                    var response = await _httpClient.GetAsync($"https://localhost:7478/api/{apiEndpoint}/including-deleted/{entry.GetPrimaryKey()}");

                    if (response.IsSuccessStatusCode)
                    {
                        var serverDto = await response.Content.ReadFromJsonAsync<TDto>();
                        var serverEntity = _mapper.Map<T>(serverDto);

                        if (entry.ETag != serverEntity.ETag && entry.LastModified > serverEntity.LastModified)
                        {
                            await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/{apiEndpoint}/including-deleted/{entry.GetPrimaryKey()}", dto);
                            await _sqliteService.MarkAsSyncedAsync(entry.GetPrimaryKey());
                        }
                    }
                    else
                    {
                        await _httpClient.PostAsJsonAsync($"https://localhost:7478/api/{apiEndpoint}", dto);
                        await _sqliteService.MarkAsSyncedAsync(entry.GetPrimaryKey());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Sync Error] {typeof(T).Name}: {ex.Message}");
            }
        }
    }
}
