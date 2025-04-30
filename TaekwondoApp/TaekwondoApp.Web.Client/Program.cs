using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaekwondoApp.Shared.ServiceInterfaces;
using TaekwondoApp.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the TaekwondoApp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
