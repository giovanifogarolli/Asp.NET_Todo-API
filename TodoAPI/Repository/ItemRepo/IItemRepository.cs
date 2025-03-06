using System.Linq.Expressions;
using TodoAPI.Model;
using TodoAPI.Repository.GenericRepo;
using TodoAPI.Utils.Pagination;

namespace TodoAPI.Repository.Itens;

public interface IItemRepository : IGenericRepository<Item>
{
    Task<Item> Get(Expression<Func<Item, bool>> predicate, int id);
    Task<PagedList<Item>> GetItemPorCategoria(int idCategoria, int idUser, QueryParameters itensParameters);

    Task<PagedList<Item>> GetItens(QueryParameters itensParameters, int id);

}
