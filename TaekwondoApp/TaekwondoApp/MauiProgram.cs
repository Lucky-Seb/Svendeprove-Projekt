using Microsoft.Extensions.Logging;
using TaekwondoApp.Services;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Shared.Mapping;
using TaekwondoApp.Shared.ServiceInterfaces;
using TaekwondoApp.Shared.Models;


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

            // Device-specific services
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            // HttpClientFactory registration
            builder.Services.AddHttpClient(); // fallback/default client

            // Scoped auth service
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Register JWT Auth message handler
            builder.Services.AddScoped<JwtAuthMessageHandler>();

            // Authenticated HttpClient (named "ApiClient")
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7478/");
            }).AddHttpMessageHandler<JwtAuthMessageHandler>();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(OrdbogMap));
            builder.Services.AddAutoMapper(typeof(BrugerMap));

            // SQLite
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ordbog.db");
            builder.Services.AddSingleton<ISQLiteService>(new SQLiteService(dbPath));
            builder.Services.AddSingleton(typeof(IGenericSQLiteService<>), typeof(GenericSQLiteService<>));
            builder.Services.AddSingleton<IBrugerSQLiteService, BrugerSQLiteService>();

            // Sync services
            builder.Services.AddSingleton<IOrdbogSyncService, OrdbogSyncService>();
            builder.Services.AddSingleton(typeof(IGenericSyncService<,>), typeof(GenericSyncService<,>));
            builder.Services.AddSingleton<IBrugerSyncService, BrugerSyncService>();

            // Blazor
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
