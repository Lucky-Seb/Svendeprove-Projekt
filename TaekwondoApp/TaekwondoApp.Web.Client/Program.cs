using TaekwondoApp.Shared.Services;
using TaekwondoApp.Shared.ServiceInterfaces;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Mapping;
using FluentValidation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaekwondoApp.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Shared services
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDTO>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<ITokenStorage, ServerT>(); // Web-compatible storage (localStorage/sessionStorage)

// JWT handler
builder.Services.AddScoped<JwtAuthMessageHandler>();

// HttpClient for API
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7478/");
}).AddHttpMessageHandler<JwtAuthMessageHandler>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// SignalR (if needed in Blazor WebAssembly)
builder.Services.AddSingleton(sp =>
{
    var hubUrl = "https://localhost:7478/ordboghub";
    return new SignalRService(hubUrl);
});

await builder.Build().RunAsync();
