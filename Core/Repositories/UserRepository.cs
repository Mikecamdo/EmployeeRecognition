using Microsoft.EntityFrameworkCore;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Database.Context;
using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MySqlDbContext _mySqlDbContext;

    public UserRepository(MySqlDbContext mySqlDbContext)
    {
        _mySqlDbContext = mySqlDbContext;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _mySqlDbContext.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        return await _mySqlDbContext.Users.FirstOrDefaultAsync(e => e.Id == userId);
    }

    public async Task<User?> GetUserByLoginCredentialAsync(LoginCredential loginCredential)
    {
        return await _mySqlDbContext.Users
            .FirstOrDefaultAsync(e => e.Name == loginCredential.Name && e.Password == loginCredential.Password);
    }

    public async Task<User> AddUserAsync(User user)
    {
        _mySqlDbContext.Users.Add(user);
        await _mySqlDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _mySqlDbContext.Users.Update(user);
        await _mySqlDbContext.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserAsync(User user)
    {
        _mySqlDbContext.Users.Remove(user);
        await _mySqlDbContext.SaveChangesAsync();
    }
}
