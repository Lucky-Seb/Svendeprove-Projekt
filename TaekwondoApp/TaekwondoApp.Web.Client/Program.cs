using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the TaekwondoApp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton<SQLiteService>();  // Register SQLiteService here

await builder.Build().RunAsync();
