using Veda.Application.Ports.Storage.Paths;

namespace Veda.Application.Ports.Storage;

public interface IStorageAccessorFactory
{
    IStorageAccessor Generate(RelativePath path);
}