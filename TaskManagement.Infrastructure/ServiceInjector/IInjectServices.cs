using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Infrastructure.ServiceInjector;
public interface IInjectServices
{
    void Configure(IServiceCollection services);
}
