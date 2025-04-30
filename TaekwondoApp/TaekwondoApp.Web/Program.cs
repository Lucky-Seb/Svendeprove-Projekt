using TaekwondoApp.Shared.Services;
using TaekwondoApp.Web.Components;
using TaekwondoApp.Shared.ServiceInterfaces;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Mapping;
using FluentValidation;
using TaekwondoApp.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add Razor components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Shared services
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDTO>();
builder.Services.AddScoped<IAuthStateProvider, AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddSingleton<ITokenStorage, ServerTokenStorage>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<JwtAuthMessageHandler>();
builder.Services.AddSingleton<ITokenStorage, ServerTokenStorage>(); // Implement WebTokenStorage for browser use

// JWT handler (optional on server side)
builder.Services.AddScoped<JwtAuthMessageHandler>();

// HttpClient for API
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7478/");
}).AddHttpMessageHandler<JwtAuthMessageHandler>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// SignalR (optional if used on server)
builder.Services.AddSingleton(sp =>
{
    var hubUrl = "https://localhost:7478/ordboghub";
    return new SignalRService(hubUrl);
});

var app = builder.Build();

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
