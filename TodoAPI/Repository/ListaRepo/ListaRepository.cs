using System.Linq.Expressions;
using TodoAPI.Context;
using TodoAPI.Model;
using TodoAPI.Repository.GenericRepo;
using TodoAPI.Repository.ListaRepo;

namespace TodoAPI.Repository.Categoria;

public class ListaRepository : GenericRepository<Lista>,IListaRepository
{

    public ListaRepository(AppDbContext context) : base(context)
    {
    }


}
