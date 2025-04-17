using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.ServiceInterfaces;

public class BrugerSyncService
{
    private readonly IGenericSQLiteService<Bruger> _sqliteService;
    private readonly IGenericSyncService<Bruger, BrugerDTO> _syncService;

    public BrugerSyncService(
        IGenericSQLiteService<Bruger> sqliteService,
        IGenericSyncService<Bruger, BrugerDTO> syncService)
    {
        _sqliteService = sqliteService;
        _syncService = syncService;
    }

    public async Task SyncAsync()
    {
        await _syncService.SyncDataAsync("bruger");
    }
}
