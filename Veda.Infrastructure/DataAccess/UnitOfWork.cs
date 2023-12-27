using Veda.Application.Abstract;
using Veda.Application.DatabaseAccess;

namespace Veda.Infrastructure.DataAccess;

public class UnitOfWork(VedaDbContext context) : IUnitOfWork
{
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
    {
        return new Repository<TEntity>(context);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void BeginTransaction()
    {
        context.Database.BeginTransaction();
    }

    public void Commit()
    {
        SaveChanges();
        context.Database.CommitTransaction();
    }

    public void Rollback()
    {
        context.Database.RollbackTransaction();
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}
