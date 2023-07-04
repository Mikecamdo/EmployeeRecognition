using Microsoft.EntityFrameworkCore;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Database.Context;

namespace EmployeeRecognition.Core.Repositories;

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

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _mySqlDbContext.Users.ToListAsync();
    }
}
