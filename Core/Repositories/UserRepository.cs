using EmployeeRecognition.Database.EntityFramework;
using EmployeeRecognition.Interfaces;
using EmployeeRecognition.Database;

namespace EmployeeRecognition.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MySqlDbContext _mySqlDbContext;

    public UserRepository(MySqlDbContext mySqlDbContext)
    {
        _mySqlDbContext = mySqlDbContext;
    }
    public async Task<User> AddUserAsync(User user)
    {
        _mySqlDbContext.Users.Add(user);
        await _mySqlDbContext.SaveChangesAsync();
        return user;
    }
}
