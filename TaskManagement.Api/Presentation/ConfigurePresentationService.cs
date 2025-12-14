using Serilog;
using TaskManagement.Api.Presentation.Exeption;
using TaskManagement.Api.Presentation.InjectService;

namespace TaskManagement.Api.Presentation;

public static class ConfigurePresentationService
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        
        services
            .AddExceptionHandler<AppExceptionHandler>();

        services
            .AddSerilog((serviceProvider, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(serviceProvider)
                .Enrich.FromLogContext()
            );

        services
            .AddInjectServices()
            .AddInjectServicesWithConfiguration(builder.Configuration)
            .AddBaseHandlerConfiguration()
            .AddEndpointConfiguration()
            .AddJsonConfiguration()
            .AddOpenApiConfiguration()
            .AddAuthConfiguration(builder.Configuration);

        services
            .AddOptions();

        return services;
    }
}
