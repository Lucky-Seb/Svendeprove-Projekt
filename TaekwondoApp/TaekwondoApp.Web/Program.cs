using TaekwondoApp.Shared.Services;
using TaekwondoApp.Web.Components;
using TaekwondoApp.Shared.ServiceInterfaces;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Mapping;
using FluentValidation;
using TaekwondoApp.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Razor components (for Blazor Server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents(); // If you need to support WebAssembly components as well

// Shared services
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDTO>();

// Register token storage for server-side use
builder.Services.AddSingleton<ITokenStorage, ServerTokenStorage>();

// Auth State
builder.Services.TryAddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.TryAddScoped<IAuthStateProvider, AuthStateProvider>();
builder.Services.TryAddScoped<IAuthenticationService, AuthenticationService>();

// Check if services are registered correctly
CheckServiceRegistration(builder.Services);

// JWT Auth Message Handler
builder.Services.AddScoped<JwtAuthMessageHandler>();

// HttpClient for API with JWT Auth handler
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7478/"); // Your API base address
}).AddHttpMessageHandler<JwtAuthMessageHandler>();

// AutoMapper for DTO and model mappings
builder.Services.AddAutoMapper(typeof(MappingProfile));

// SignalR Service (optional if using SignalR)
builder.Services.AddSingleton(sp =>
{
    var hubUrl = "https://localhost:7478/ordboghub"; // Your SignalR hub URL
    return new SignalRService(hubUrl);
});

// Optionally configure SSL validation in development
builder.Services.AddSingleton<HttpMessageHandler>(_ =>
{
    if (builder.Environment.IsDevelopment())
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
    }
    return new HttpClientHandler();
});

var app = builder.Build();

// Development-specific configuration
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Middleware pipeline configuration
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();  // Anti-forgery protection

// Map Razor components and render modes for server-side Blazor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()  // Interactive rendering mode
    .AddInteractiveWebAssemblyRenderMode()  // WebAssembly mode for components
    .AddAdditionalAssemblies(
        typeof(TaekwondoApp.Shared._Imports).Assembly,
        typeof(TaekwondoApp.Web.Client._Imports).Assembly);

app.Run();

// Function to check if services are registered
void CheckServiceRegistration(IServiceCollection services)
{
    // Check for IAuthenticationService
    var authService = services.FirstOrDefault(service => service.ServiceType == typeof(IAuthenticationService));
    if (authService != null)
    {
        Console.WriteLine("IAuthenticationService is registered.");
    }
    else
    {
        Console.WriteLine("IAuthenticationService is NOT registered.");
    }

    // Check for IAuthStateProvider
    var authStateProvider = services.FirstOrDefault(service => service.ServiceType == typeof(IAuthStateProvider));
    if (authStateProvider != null)
    {
        Console.WriteLine("IAuthStateProvider is registered.");
    }
    else
    {
        Console.WriteLine("IAuthStateProvider is NOT registered.");
    }

    // Check for AuthenticationStateProvider
    var authState = services.FirstOrDefault(service => service.ServiceType == typeof(AuthenticationStateProvider));
    if (authState != null)
    {
        Console.WriteLine("AuthenticationStateProvider is registered.");
    }
    else
    {
        Console.WriteLine("AuthenticationStateProvider is NOT registered.");
    }
}
