using Microsoft.Extensions.DependencyInjection;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Web.Components;
using TaekwondoApp.Web.Services;
using TaekwondoApp.Shared.Mapping;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);
// Scoped auth service
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
// Register JWT Auth message handler
builder.Services.AddScoped<JwtAuthMessageHandler>();

// Auth state provider (if using CascadingAuthenticationState)
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AuthStateProvider>());

// Update the HttpClient registration to pass the required IAuthenticationService to JwtAuthMessageHandler
// Register a named HttpClient with JwtAuthMessageHandler
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7478/"); // Set the API base URL here
});
//.AddHttpMessageHandler<JwtAuthMessageHandler>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add service defaults & Redis cache
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => options.DetailedErrors = true);

// Register Razor components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Device-specific service
builder.Services.AddSingleton<IFormFactor, FormFactor>();

var app = builder.Build();
// Register AuthStateProvider as both AuthenticationStateProvider and itself


if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(TaekwondoApp.Shared._Imports).Assembly,
        typeof(TaekwondoApp.Web.Client._Imports).Assembly);

app.Run();
