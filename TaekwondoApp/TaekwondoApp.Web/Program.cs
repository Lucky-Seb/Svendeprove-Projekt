using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TaekwondoApp.Web.Components;
using TaekwondoApp.Web.Services;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Shared.Mapping;
using TaekwondoApp.Shared.DTO;
using FluentValidation;
using System.Net.Http;
using TaekwondoApp.Shared.ServiceInterfaces;
using Microsoft.AspNetCore.Components.Forms; // Required for forms and antiforgery

var builder = WebApplication.CreateBuilder(args);

// ========== SERVICES REGISTRATION ==========

// Scoped auth service
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// JWT auth message handler
builder.Services.AddScoped<JwtAuthMessageHandler>();

// Register ITokenStorage for Server-side storage
builder.Services.AddSingleton<ITokenStorage, ServerTokenStorage>();

// Auth state provider for Blazor
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AuthStateProvider>());
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

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

// Add Blazor server services (this includes the AntiforgeryStateProvider and PersistentComponentState automatically)
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => options.DetailedErrors = true);

// Register antiforgery services (default behavior)
builder.Services.AddAntiforgery(options =>
{
    // Customize antiforgery options if needed
});

// ========== BUILD AND CONFIGURE PIPELINE ==========

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Logger.LogInformation("Running in Development");
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

builder.Services.AddLogging(logging =>
{
    logging.AddConsole(); // Logs to the console
    logging.AddDebug();   // Logs to the debug output
    logging.AddEventSourceLogger(); // Logs to event source
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();  // Enables antiforgery protection globally
app.MapStaticAssets();

// Map Blazor server components
app.MapBlazorHub(); // This is necessary for Blazor Server-side
app.MapFallbackToPage("/_Host");

app.Run();
