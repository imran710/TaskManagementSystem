using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Domain.Helper;

public interface IInjectServicesWithConfiguration
{
    void Configure(IServiceCollection services, IConfiguration configuration);
}