using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Veda.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        // register other services
        
        return services;
    }
}