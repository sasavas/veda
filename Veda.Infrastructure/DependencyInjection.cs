using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Veda.Application.DatabaseAccess;
using Veda.Application.Ports;
using Veda.Infrastructure.DataAccess;
using Veda.Infrastructure.DataAccess.RepositoryAdapters;
using Veda.Infrastructure.ServiceImplementations;
using Veda.SharedKernel.Services.Email;
using Veda.SharedKernel.Services.HtmlHelper;

namespace Veda.Infrastructure;

public static class DependencyInjection
{
    // public static IServiceProvider ServiceProvider = null!;

    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services)
    {
        // Database Infrastructure
        services.AddDbContext<VedaDbContext>(
            options =>
            {
                // options.UseLazyLoadingProxies();
                options.EnableSensitiveDataLogging();
                options.UseMySql(
                        "server=34.123.120.77;database=veda;user=root;password=1460", 
                        new MySqlServerVersion(new Version(8, 0, 31)))
                    .UseSnakeCaseNamingConvention();
                //TODO: add connectionString via config later
            });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRecipientRepository, RecipientRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        
        // Other Infrastructure Services
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IHtmlService, HtmlService>();
        services.AddTransient<IHtmlBuilder, HtmlBuilder>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        // ServiceProvider = services.BuildServiceProvider();

        return services;
    }
}