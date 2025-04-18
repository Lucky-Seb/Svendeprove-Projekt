using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text;
using System.Text.Json;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Mapping;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Filters;
using TaekwondoOrchestration.ApiService.Middlewares;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoOrchestration.ApiService.Validators;
using TaekwondoOrchestration.ApiService.Helpers;

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
var repositoryTypes = typeof(IBrugerKlubRepository).Assembly
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
var serviceTypes = new[]
{
    typeof(BrugerKlubService), typeof(BrugerØvelseService), typeof(BrugerProgramService),
    typeof(BrugerQuizService), typeof(BrugerService), typeof(KlubService),
    typeof(KlubProgramService), typeof(KlubQuizService), typeof(KlubØvelseService),
    typeof(OrdbogService), typeof(ØvelseService), typeof(PensumService),
    typeof(ProgramPlanService), typeof(QuizService), typeof(SpørgsmålService),
    typeof(TeknikService), typeof(TeoriService), typeof(TræningService)
};

foreach (var serviceType in serviceTypes)
{
    builder.Services.AddScoped(serviceType);
}

// Register IJwtHelper and JwtHelper for token generation
builder.Services.AddSingleton<IJwtHelper>(new JwtHelper(builder.Configuration["Jwt:SecretKey"])); // Use the secret key from the configuration

// Optional: Register a specific service using an interface
builder.Services.AddScoped<IOrdbogService, OrdbogService>();
builder.Services.AddScoped<IBrugerService, BrugerService>();

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

// ---------------------
// ✅ FluentValidation
// ---------------------
builder.Services.AddValidatorsFromAssemblyContaining<BrugerDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PensumDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrdbogDTOValidator>();
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

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.AddServiceDefaults();

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

// ---------------------
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
