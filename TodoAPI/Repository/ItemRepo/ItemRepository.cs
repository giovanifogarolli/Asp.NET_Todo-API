using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TodoAPI.Context;
using TodoAPI.Model;
using TodoAPI.Repository.GenericRepo;
using TodoAPI.Repository.Itens;
using TodoAPI.Utils.Pagination;

namespace TodoAPI.Repository.item;

public class ItemRepository : GenericRepository<Item>, IItemRepository
{

    public ItemRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Item>> GetItemPorCategoria(int idCategoria, int idUser, QueryParameters itensParameters)
    {

        IEnumerable<Item> itens = await GetAll(idUser);

        var itensFiltrados = itens.Where(i => i.listaId == idCategoria).AsQueryable();

        return PagedList<Item>.toPagedList(itensFiltrados, itensParameters.PageNumber, itensParameters.Pagesize);
    }

    public async Task<Item?> Get(Expression<Func<Item, bool>> predicate, int id)
    {
        return await _context.Set<Item>()
                    .Include(i => i.lista)
                    .ThenInclude(l => l.User)
                    .Where(i => i.lista.UserId == id)
                    .FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Item>> GetAll(int id)
    {
        return await _context.Set<Item>()
                    .Include(i => i.lista)
                    .ThenInclude(l => l.User)
                    .Where(i => i.lista.UserId == id)
                    .ToListAsync();
    }

    public async Task<PagedList<Item>> GetItens(QueryParameters itensParameters, int id)
    {
        IEnumerable<Item> itens = await GetAll(id);

        var itensOrdenados = itens.OrderBy(on => on.titulo).AsQueryable();

        return PagedList<Item>.toPagedList(itensOrdenados, itensParameters.PageNumber, itensParameters.Pagesize);
    }

}
