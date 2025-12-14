using TaskManagement.Application;
using TaskManagement.Infrastructure.ServiceInjector;

namespace TaskManagement.Api.Presentation.InjectService;

public static class InjectServices
{
    public static IServiceCollection AddInjectServices(this IServiceCollection services)
    {
        var injectServices = new ServiceCollection();

        injectServices.Scan(scan =>
            scan
                .FromAssembliesOf(typeof(IApiAssemblyMarker), typeof(ICoreAssemblyMarker))
                .AddClasses(classes => classes.AssignableTo<IInjectServices>())
                .AsImplementedInterfaces()
        );

        var serviceProvider = injectServices.BuildServiceProvider();
        foreach (var item in serviceProvider.GetRequiredService<IEnumerable<IInjectServices>>())
        {
            item.Configure(services);
        }

        return services;
    }
}
