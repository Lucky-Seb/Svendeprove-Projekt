using Microsoft.Extensions.Logging;
using TaekwondoApp.Services;
using TaekwondoApp.Shared.Services;
using Microsoft.Maui.Storage;
using System.IO;
using Microsoft.AspNetCore.Components;

namespace TaekwondoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the TaekwondoApp.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddHttpClient();  // Register IHttpClientFactory
            // Register OrdbogSyncService and pass IHttpClientFactory to it
            builder.Services.AddSingleton<IGenericSyncService, GenericSyncService>();
            builder.Services.AddSingleton<IOrdbogSyncService, OrdbogSyncService>();

            // Configure SQLite service with the database path
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ordbog.db");

            // Register SQLiteService as a singleton with the database path
            builder.Services.AddSingleton<ISQLiteService>(new SQLiteService(dbPath));

            // Register HttpClientFactory to handle HttpClient instances
            builder.Services.AddHttpClient();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
