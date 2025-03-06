using System.Linq.Expressions;
using TodoAPI.Model;
using TodoAPI.Repository.ListaRepo;

namespace TodoAPI.Services;

public class ListaService : IListaRepository
{
    public Lista Create(Lista Entity)
    {
        
    }

    public Lista Delete(Lista Entity)
    {
        throw new NotImplementedException();
    }

    public Task<Lista?> Get(Expression<Func<Lista, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Lista>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Lista Update(Lista Entity)
    {
        throw new NotImplementedException();
    }
}
