using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TodoAPI.Context;
using TodoAPI.Model;
using TodoAPI.Repository.GenericRepo;
using TodoAPI.Repository.UserRepo;


namespace TodoAPI.Repository.UserRepo;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> findByName(String username)
    {
       return await _context.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
    }
}
