using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Veda.Infrastructure.DataAccess;

namespace Veda.IntegaretionTest.Abstract;

public class TestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public static ServiceProvider ServiceProvider { get; private set; } = null!;
    
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("veda")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithCleanUp(true)
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            OverrideDbContext(services);
            
            ServiceProvider = services.BuildServiceProvider();
        });
    }
    
    private void OverrideDbContext(IServiceCollection services)
    {
        var depDbContext = services
            .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<VedaDbContext>));
        if (depDbContext is not null) services.Remove(depDbContext);
        services.AddDbContext<VedaDbContext>(options =>
        {
            var connStr = _dbContainer.GetConnectionString();
            options.UseNpgsql(connStr).UseSnakeCaseNamingConvention();
        });
    }

    public Task InitializeAsync() => _dbContainer.StartAsync();

    public new Task DisposeAsync() => _dbContainer.DisposeAsync().AsTask();
}