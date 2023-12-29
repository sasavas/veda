using System.Linq.Expressions;
using Veda.Application.Abstract;

namespace Veda.Application.DatabaseAccess;

public interface IRepository<TEntity> 
    where TEntity : Entity
{
    TEntity? GetById(int id);

    IEnumerable<TEntity> GetAll();
    
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
    
    TEntity? GetUnique(Expression<Func<TEntity, bool>> filter);

    TEntity Create(TEntity entity);

    TEntity Update(TEntity entity);

    void Delete(int entityId);
}