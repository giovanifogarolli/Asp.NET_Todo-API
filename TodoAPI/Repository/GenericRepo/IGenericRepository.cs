using System.Linq.Expressions;

namespace TodoAPI.Repository.GenericRepo;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(Expression<Func<T, bool>> predicate);
    T Create(T Entity);
    T Update(T Entity);
    T Delete(T Entity);

}
