using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TodoAPI.Context;

namespace TodoAPI.Repository.GenericRepo;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{

    protected readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public T Create(T Entity)
    {
        _context.Set<T>().Add(Entity);

        return Entity;
    }

    public T Delete(T Entity)
    {
        _context.Set<T>().Remove(Entity);

        return Entity;
    }

    public async Task<T?> Get(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public T Update(T Entity)
    {
        _context.Set<T>().Update(Entity);

        return Entity;
    }
}
