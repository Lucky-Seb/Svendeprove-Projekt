﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Polly;
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
using FluentValidation.AspNetCore;
using FluentValidation;

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
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])
            )
        };
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:7072") // Or your frontend's URL
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Only if you're sending cookies/auth headers
    });
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

// Add configuration (appsettings.json)
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Register JwtHelper with DI, passing the secret key
builder.Services.AddSingleton<IJwtHelper>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var secretKey = configuration.GetValue<string>("Jwt:SecretKey");
    return new JwtHelper(secretKey);
});

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
builder.Services.AddAutoMapper(typeof(MappingProfile));

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
// 🧠 EF Core & DB Context with Resiliency & Retry Logic
// ---------------------
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3, // Retry 3 times
            TimeSpan.FromSeconds(5), // 5 seconds delay between retries
            errorNumbersToAdd: null); // Optionally specify specific SQL error codes to retry on
    }));

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
app.UseMiddleware<GlobalExceptionMiddleware>(); // Add this first for exception handling

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Taekwondo Orchestration API V1");
        c.RoutePrefix = string.Empty;
    });
    app.UseCors("AllowLocalhostFrontend");
    app.MapHub<OrdbogHub>("/ordbogHub");
    app.MapHub<BrugerHub>("/brugerhub");
}

// ---------------------
// 🔐 Auth & Routing
// ---------------------
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();