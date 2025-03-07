using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TodoAPI.Context;
using TodoAPI.Model;
using TodoAPI.Repository.GenericRepo;
using TodoAPI.Repository.ListaRepo;
using TodoAPI.Utils.Pagination;

namespace TodoAPI.Repository.Categoria;

public class ListaRepository : GenericRepository<Lista>, IListaRepository
{

    public ListaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Lista?> Get(Expression<Func<Lista, bool>> predicate, int id)
    {
        return await _context.Set<Lista>()
            .Include(l => l.itens)
            .Where(l => l.UserId == id)
            .FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Lista>> GetAll(int userId)
    {
        return await _context.Set<Lista>()
            .Include(l => l.itens)
            .Where(l => l.UserId == userId)
            .ToListAsync();
    }

}
