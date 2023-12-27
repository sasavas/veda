using Veda.Application.Abstract;

namespace Veda.Application.SharedKernel.Exceptions;

public class NotFoundException<TEntity> : DomainException where TEntity : Entity
{
    public NotFoundException() : base(nameof(TEntity) + " not found")
    {
    }

    public NotFoundException(Exception? innerException) : base(nameof(TEntity) + " not found", innerException)
    {
    }
}