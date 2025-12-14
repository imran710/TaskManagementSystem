using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Helper;

namespace TaskManagement.Application.Helper;
public class ConfigureHelperService : IInjectServicesWithConfiguration
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<SecurityHelper>();
        services.AddSingleton<IDateTimeHelper, DateTimeHelper>();
    }
}
