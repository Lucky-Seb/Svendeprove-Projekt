using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Mapping;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Filters;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoOrchestration.ApiService.Validators;
using TaekwondoOrchestration.ApiService.Helpers;
using System.Text.Json;
using TaekwondoOrchestration.ApiService.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// ---------------------
// 🔐 Authentication
// ---------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "YourIssuer",
            ValidAudience = "YourAudience",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]) // Using the key from configuration
            )
        };
    });

builder.Services.AddAuthorization(); // Add custom policies here if needed

// ---------------------
// 🗂 Repositories
// ---------------------
var repositoryTypes = typeof(IBrugerRepository).Assembly
    .GetTypes()
    .Where(t => t.Name.EndsWith("Repository") && !t.IsInterface);

foreach (var repo in repositoryTypes)
{
    var interfaceType = repo.GetInterfaces().FirstOrDefault(i => i.Name == $"I{repo.Name}");
    if (interfaceType != null)
    {
        builder.Services.AddScoped(interfaceType, repo);
    }
}

// ---------------------
// 🧠 Services
// ---------------------
var serviceTypes = typeof(IBrugerService).Assembly
    .GetTypes()
    .Where(t => t.Name.EndsWith("Service") && !t.IsInterface);

foreach (var service in serviceTypes)
{
    var interfaceType = service.GetInterfaces().FirstOrDefault(i => i.Name == $"I{service.Name}");
    if (interfaceType != null)
    {
        builder.Services.AddScoped(interfaceType, service);
    }
}

// ---------------------
// 🧭 Middleware & Filters
// ---------------------
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
}).AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// ---------------------
// 🧰 Utility & AutoMapper
// ---------------------
builder.Services.AddAutoMapper(typeof(BrugerMap));
builder.Services.AddAutoMapper(typeof(OrdbogMap));
builder.Services.AddAutoMapper(typeof(KlubMap));
builder.Services.AddAutoMapper(typeof(ProgramPlanMap));
builder.Services.AddAutoMapper(typeof(PensumMap));
builder.Services.AddAutoMapper(typeof(ØvelseMap));

// ---------------------
// ✅ FluentValidation
// ---------------------
builder.Services.AddValidatorsFromAssemblyContaining<BrugerDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PensumDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrdbogDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ØvelseDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProgramPlanDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<KlubDTOValidator>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters(); // Optional

// ---------------------
// 🧠 EF Core & DB Context
// ---------------------
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------------
// 📡 SignalR
// ---------------------
builder.Services.AddSignalR();

// ---------------------
// 🔎 Swagger / OpenAPI
// ---------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Taekwondo Orchestration API",
        Version = "v1"
    });
});

// ---------------------
// 🔧 Build App
// ---------------------
var app = builder.Build();

// ---------------------
// 🔥 Middleware
// ---------------------
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Taekwondo Orchestration API V1");
        c.RoutePrefix = string.Empty;
    });

    app.MapHub<OrdbogHub>("/ordbogHub");
}

// ---------------------
// 🔐 Auth & Routing
// ---------------------
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ---------------------
// 🌤 Test Endpoint
// ---------------------
string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
