using Veda.Application.Abstract;

namespace Veda.Application.Ports.DataAccess;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
    void SaveChanges();
    void BeginTransaction();
    void Commit();
    void Rollback();
}