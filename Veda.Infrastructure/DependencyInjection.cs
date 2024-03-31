using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Veda.Application.Ports;
using Veda.Application.Ports.DataAccess;
using Veda.Application.Ports.Storage;
using Veda.Infrastructure.DataAccess;
using Veda.Infrastructure.DataAccess.RepositoryAdapters;
using Veda.Infrastructure.ServiceImplementations;
using Veda.Infrastructure.Storage;
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
                // options.EnableSensitiveDataLogging();
                options.UseInMemoryDatabase("veda");
                    //UseNpgsql(
                    //"Server=34.140.112.19;Port=5432;Database=veda;User Id=postgres;Password=1460;Include Error Detail=True;")
                    //.UseSnakeCaseNamingConvention();
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
        services.AddTransient<IStorageAccessorFactory, LocalStorageAccessorFactory>();
        services.AddTransient<IFileHasher, DummyFileHasher>();
        
        // ServiceProvider = services.BuildServiceProvider();

        return services;
    }
}