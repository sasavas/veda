using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Veda.Application.DatabaseAccess;
using Veda.Application.Ports;
using Veda.Infrastructure.DataAccess;
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
                options.UseLazyLoadingProxies();
                options.EnableSensitiveDataLogging();
                options.UseNpgsql(
                        "Server=127.0.0.1;Port=5432;Database=veda;User Id=postgres;Password=1460;Include Error Detail=True;")
                    .UseSnakeCaseNamingConvention();
                //TODO: add connectionString via config later
            });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Other Infrastructure Services
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IHtmlService, HtmlService>();
        services.AddTransient<IHtmlBuilder, HtmlBuilder>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        // ServiceProvider = services.BuildServiceProvider();

        return services;
    }
}