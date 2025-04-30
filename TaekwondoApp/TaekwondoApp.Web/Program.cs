using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaekwondoApp.Web.Components;
using TaekwondoApp.Web.Services;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Shared.Mapping;
using TaekwondoApp.Shared.DTO;
using FluentValidation;
using System.Net.Http;
using TaekwondoApp.Shared.ServiceInterfaces;

var builder = WebApplication.CreateBuilder(args);

// ========== SERVICES REGISTRATION ==========

// Scoped auth service
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// JWT auth message handler
builder.Services.AddScoped<JwtAuthMessageHandler>();

// Auth state provider for Blazor
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AuthStateProvider>());

// Register HttpClient with JWT handler
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7478/");
})
.AddHttpMessageHandler<JwtAuthMessageHandler>();

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

// FluentValidation for shared DTOs
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDTO>();

// SignalR service (optional if used in shared logic)
builder.Services.AddSingleton(sp =>
{
    var hubUrl = "https://localhost:7478/ordboghub";
    return new SignalRService(hubUrl);
});

// Device-specific abstraction (if applicable in shared UI)
builder.Services.AddSingleton<IFormFactor, FormFactor>();

// Add Blazor server services
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => options.DetailedErrors = true);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// ========== BUILD AND CONFIGURE PIPELINE ==========

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.Logger.LogInformation("Running in Development");
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseStaticFiles();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(TaekwondoApp.Shared._Imports).Assembly,
        typeof(TaekwondoApp.Web.Client._Imports).Assembly);

app.Run();
