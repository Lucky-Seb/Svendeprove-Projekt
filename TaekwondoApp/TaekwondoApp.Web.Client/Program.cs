using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaekwondoApp.Shared.Mapping;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the TaekwondoApp.Shared project
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
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Blazor
builder.Services.AddScoped<AuthStateProvider>();

await builder.Build().RunAsync();
