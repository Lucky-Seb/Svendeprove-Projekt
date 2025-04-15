using Microsoft.Extensions.Logging;
using TaekwondoApp.Services;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Shared.Mapping;
using Microsoft.Maui.Storage;
using System.IO;
using Microsoft.AspNetCore.Components;
using static TaekwondoApp.Services.AuthenticationService;

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
            builder.Services.AddAutoMapper(typeof(OrdbogMap)); // Register the profile
            // Configure SQLite service with the database path
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ordbog.db");
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            // Register SQLiteService as a singleton with the database path
            builder.Services.AddSingleton<ISQLiteService>(new SQLiteService(dbPath));
            // Register HttpClientFactory to handle HttpClient instances
            builder.Services.AddScoped<JwtAuthMessageHandler>();
            builder.Services.AddHttpClient("ApiClient")
                .AddHttpMessageHandler<JwtAuthMessageHandler>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:7478/"));
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<AuthStateProvider>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
