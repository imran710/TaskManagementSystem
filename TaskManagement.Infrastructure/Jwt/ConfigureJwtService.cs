

using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Jwt;
using TaskManagement.Infrastructure.ServiceInjector;

namespace Core.Infrastructure.Jwt;

public class ConfigureJwtService : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddOptions<JwtOption>().BindConfiguration(JwtOption.SectionName).ValidateOnStart();
    }
}

