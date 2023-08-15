using Microsoft.EntityFrameworkCore;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Entities;
using Laudatio.Database.Context;
using Laudatio.Api.Models;

namespace Laudatio.Core.Repositories;

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

    public async Task<User?> GetUserByNameAsync(string name)
    {
        return await _mySqlDbContext.Users.FirstOrDefaultAsync(e => e.Name == name);
    }


    public async Task<User?> GetUserByLoginCredentialAsync(LoginCredential loginCredential)
    {
        var user = await _mySqlDbContext.Users.FirstOrDefaultAsync(e => e.Name == loginCredential.Name);

        if (user == null)
        {
            return null;
        }

        bool validPassword = BCrypt.Net.BCrypt.Verify(loginCredential.Password, user.Password);
        
        if (validPassword)
        {
            return user;
        }

        return null;
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
