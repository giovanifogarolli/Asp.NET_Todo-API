namespace TodoAPI.Repository.UserRepo;
using TodoAPI.Model;
using TodoAPI.Repository.GenericRepo;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User> findByName(String username);

}
