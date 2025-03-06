using TodoAPI.Context;
using TodoAPI.Repository.item;
using TodoAPI.Repository.Itens;
using TodoAPI.Repository.UserRepo;

namespace TodoAPI.Repository.UnityOfWork;

public class UnitOfWork : IUnitOfWork
{
    private IItemRepository? _itemRepo;
    private IUserRepository? _UserRepo;
    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IItemRepository ItemRepository
    {
        get
        {
            return _itemRepo = _itemRepo ?? new ItemRepository(_context);
        }
    }

    public IUserRepository UserRepository
    {
        get{
            return _UserRepo = _UserRepo ?? new UserRepository(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
