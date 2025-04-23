using Microsoft.Extensions.Logging;
using TaekwondoApp.Services;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Shared.Mapping;


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

            // Device-specific services do not use @ in git commets
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            // HttpClientFactory registration
            builder.Services.AddHttpClient(); // fallback/default client

            // Scoped auth service
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Register JWT Auth message handler
            builder.Services.AddScoped<JwtAuthMessageHandler>();

#if ANDROID
            // Custom handler for Android to bypass SSL for dev
            builder.Services.AddSingleton<HttpMessageHandler>(_ =>
            {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };
            });

            // Register named HttpClient using the custom handler
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://10.0.2.2:7478/");
            })
            .ConfigurePrimaryHttpMessageHandler(sp => sp.GetRequiredService<HttpMessageHandler>())
            .AddHttpMessageHandler<JwtAuthMessageHandler>();

#else

            // Register standard HttpClient for other platforms
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7478/");
            })
            .AddHttpMessageHandler<JwtAuthMessageHandler>();

#endif


            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // SQLite
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ordbog.db");
            builder.Services.AddSingleton<ISQLiteService>(new SQLiteService(dbPath));

            // Sync services
            builder.Services.AddSingleton<IGenericSyncService, GenericSyncService>();
            builder.Services.AddSingleton<IOrdbogSyncService, OrdbogSyncService>();

            // Blazor
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<AuthStateProvider>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        // Custom handler that wraps HttpClientHandler for SSL bypass
    }
}
