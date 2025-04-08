using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Register services using reflection
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

// Register repositories using reflection
var repositoryTypes = typeof(IBrugerKlubRepository).Assembly
    .GetTypes()
    .Where(t => t.Name.EndsWith("Repository") && !t.IsInterface)
    .ToList();

foreach (var repo in repositoryTypes)
{
    var interfaceType = repo.GetInterfaces().FirstOrDefault(i => i.Name == "I" + repo.Name);
    if (interfaceType != null)
    {
        builder.Services.AddScoped(interfaceType, repo);
    }
}

// Add services to the container.
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Taekwondo Orchestration API",
        Version = "v1"
    });
});

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure database context and seed data

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
    DbInitializer.Seed(context);
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Taekwondo Orchestration API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
