using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using System.Text;
using TaskManagement.Api.Common;
using TaskManagement.Api.Presentation;
using TaskManagement.Api.Presentation.InjectService;
using TaskManagement.Domain.Helper;
using TaskManagement.Infrastructure.option;
using TaskManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes("vtRap!QdYv%jsqpxU*tsceQ%bt2k768q2wH");

var databaseConfig = builder.Configuration.GetSection("Database");
var connectionString = databaseConfig.GetValue<string>("ConnectionString")
                       ?? "Host=localhost;Database=MyDb;Username=postgres;Password=postgres";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog();



builder.Services.AddAuthorization();
  
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddOpenApi();
builder.Services.AddApiConfiguration(builder);
builder.Services.AddScoped<IJwtTokenHelper, JwtTokenHelper>();
builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(JwtOption.SectionName)); 

builder.Services.AddScoped<SecurityHelper>(); 

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>().ToList();
Console.WriteLine($"Endpoints found: {endpoints.Count}");


foreach (var endpoint in app.Services.GetRequiredService<IEnumerable<IEndpoint>>())
{
    endpoint.MapRoutes(app);
}

var apiOption = builder.Configuration
    .GetSection(ApiOption.SectionName)
    .Get<ApiOption>()
    ?? throw new NullReferenceException("Api Option missing");

var openApi = apiOption.OpenApi
    ?? throw new NullReferenceException("OpenApi missing");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(openApi.OpenApiRoutePattern);

    app.MapScalarApiReference(options =>
    {
        options.WithTitle(openApi.Title)
               .WithEndpointPrefix("/")
               .WithPreferredScheme(JwtBearerDefaults.AuthenticationScheme)
               .WithOpenApiRoutePattern(openApi.SubPath + openApi.OpenApiRoutePattern);
    });
}

app.Run();