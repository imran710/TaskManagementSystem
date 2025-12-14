using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Domain.Helper;

public class ConfigureHelperService : IInjectServicesWithConfiguration
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<SecurityHelper>();
        services.AddSingleton<IDateTimeHelper, DateTimeHelper>();
    }
}
