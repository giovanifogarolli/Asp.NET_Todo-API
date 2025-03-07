using System.Linq.Expressions;
using TodoAPI.Model;
using TodoAPI.Repository.GenericRepo;

namespace TodoAPI.Repository.ListaRepo;

public interface IListaRepository : IGenericRepository<Lista>
{
    public Task<Lista> Get(Expression<Func<Lista, bool>> predicate, int id);

    public Task<IEnumerable<Lista>> GetAll(int userId);
}
