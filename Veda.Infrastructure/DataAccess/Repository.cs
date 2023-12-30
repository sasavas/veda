using System.Linq.Expressions;
using Veda.Application.Abstract;
using Veda.Application.DatabaseAccess;

namespace Veda.Infrastructure.DataAccess;

public class Repository<TEntity>(VedaDbContext context) : IRepository<TEntity>
    where TEntity : Entity
{
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly VedaDbContext Context = context;

    public virtual TEntity? GetById(int id) => Context.Set<TEntity>().FirstOrDefault(entity => entity.Id == id);

    public virtual IEnumerable<TEntity> GetAll() => Context.Set<TEntity>();

    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter) => Context.Set<TEntity>().Where(filter);

    public virtual TEntity? GetUnique(Expression<Func<TEntity, bool>> filter) => Context.Set<TEntity>().FirstOrDefault(filter);
    
    public virtual TEntity Create(TEntity aggregateRoot)
    {
        var inserted = Context.Set<TEntity>().Add(aggregateRoot);
        return inserted.Entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        var updated = Context.Set<TEntity>().Update(entity);
        return updated.Entity;
    }
    
    public virtual void Delete(int entityId)
    {
        var toDelete = Context.Set<TEntity>().FirstOrDefault(t => entityId.Equals(t.Id));
        if (toDelete is not null)
        {
            Context.Set<TEntity>().Remove(toDelete);
        }
    }
}