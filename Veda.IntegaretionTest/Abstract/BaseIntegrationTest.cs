using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Veda.Infrastructure.DataAccess;

namespace Veda.IntegaretionTest.Abstract;

public abstract class BaseIntegrationTest : IClassFixture<TestWebApplicationFactory>
{
    protected TestWebApplicationFactory Factory;
    protected readonly ISender Sender;
    protected readonly VedaDbContext VedaDbContext;

    protected BaseIntegrationTest(TestWebApplicationFactory factory)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        
        Factory = factory;
        
        var scope = factory.Services.CreateScope();
        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
        VedaDbContext = scope.ServiceProvider.GetRequiredService<VedaDbContext>();
    }
}