using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Veda.Application.Modules.CustomerModule.DatabaseAccess;
using Veda.Infrastructure.DataAccess;

namespace Veda.Infrastructure;

public static class DependencyInjection
{
    public static IServiceProvider ServiceProvider;

    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services)
    {
        services.AddDbContext<VedaDbContext>(
            options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseNpgsql(
                        "Server=127.0.0.1;Port=5432;Database=veda;User Id=testuser;Password=testuser;Include Error Detail=True;")
                    .UseSnakeCaseNamingConvention();
                //TODO: add connectionString via config later
            });

        //TODO: register Repositories
        //services.AddScoped<ICustomerRepository, CustomerRepository>();

        ServiceProvider = services.BuildServiceProvider();

        return services;
    }
}