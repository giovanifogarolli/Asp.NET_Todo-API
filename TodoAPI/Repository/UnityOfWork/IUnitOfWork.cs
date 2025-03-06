using TodoAPI.Repository.Itens;
using TodoAPI.Repository.ListaRepo;
using TodoAPI.Repository.UserRepo;

namespace TodoAPI.Repository.UnityOfWork;

public interface IUnitOfWork
{
    IItemRepository ItemRepository { get; }
    IUserRepository UserRepository { get; }

    IListaRepository ListaRepository { get; }

    Task CommitAsync();

}
