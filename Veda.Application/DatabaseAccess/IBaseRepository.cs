using System.Linq.Expressions;
using Veda.Application.Abstract;

namespace Veda.Application.DatabaseAccess;

public interface IBaseRepository<TAggregateRoot> 
    where TAggregateRoot : Entity
{
    TAggregateRoot? GetById(int id);

    IEnumerable<TAggregateRoot> GetList();
    
    IEnumerable<TAggregateRoot> GetList(Expression<Func<TAggregateRoot, bool>> filter);

    TAggregateRoot? GetUnique(Expression<Func<TAggregateRoot, bool>> filter);

    TAggregateRoot Create(TAggregateRoot entity);

    TAggregateRoot Update(TAggregateRoot entity);

    void Delete(int entityId);
}