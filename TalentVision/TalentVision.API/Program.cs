using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Oracle.EntityFrameworkCore;
using TalentVision.API.Security;
using TalentVision.Application.Services;
using TalentVision.Infrastructure.Data;
using TalentVision.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHealthChecks();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IVagaService, VagaService>();
builder.Services.AddScoped<ICandidaturaService, CandidaturaService>();
builder.Services.AddScoped<ICompetenciaService, CompetenciaService>();
builder.Services.AddScoped<ILogAuditoriaService, LogAuditoriaService>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TalentVision API",
        Version = "v1",
        Description = "API de triagem de talentos (Global Solution FIAP)"
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "X-Api-Key",
        Description = "Informe a API Key no header (X-Api-Key)",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    };

    c.AddSecurityDefinition("ApiKey", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "ApiKey",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TalentVision API v1");
});


app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger("RequestLogging");

    logger.LogInformation("HTTP {method} {path}", context.Request.Method, context.Request.Path);

    await next();
});

app.UseMiddleware<ApiKeyMiddleware>();

app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
