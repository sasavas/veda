using Microsoft.Extensions.Configuration;
using Veda.Application.Ports.Storage;
using Veda.Application.Ports.Storage.Paths;

namespace Veda.Infrastructure.Storage;

public class LocalStorageAccessorFactory : IStorageAccessorFactory
{
    private readonly string basePath;
    
    public LocalStorageAccessorFactory(IConfiguration configuration)
    {
        basePath = configuration.GetSection("StoragePath")!.Value!;
    }
    
    public IStorageAccessor Generate(RelativePath path)
    {
        return new LocalStorageAccessor(Path.Combine(basePath, path.Value));
    }
}