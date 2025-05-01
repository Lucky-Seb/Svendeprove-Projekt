using TaekwondoApp.Shared.ServiceInterfaces;

namespace TaekwondoApp.Web.Services
{
    public class WebPlatformService : IPlatformSyncService
    {
        public Task SyncOrdbogAsync()
        {
            // No-op or simulated behavior
            Console.WriteLine("Sync not supported on web.");
            return Task.CompletedTask;
        }
    }
}
