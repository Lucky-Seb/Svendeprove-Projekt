using TaekwondoApp.Shared.Services;
using TaekwondoApp.Web.Components;
using TaekwondoApp.Web.Services;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Shared.Mapping;

var builder = WebApplication.CreateBuilder(args);


// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

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
builder.Services.AddSingleton<AuthStateProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
