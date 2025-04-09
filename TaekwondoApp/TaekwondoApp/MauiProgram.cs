using Microsoft.Extensions.Logging;
using TaekwondoApp.Services;
using TaekwondoApp.Shared.Services;


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
            builder.Services.AddSingleton<ISyncService, SyncService>();
            builder.Services.AddSingleton<SQLiteService>();
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient();

            return builder.Build();
        }
    }
}
