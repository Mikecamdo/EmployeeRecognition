﻿using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsersAsync();
    public Task<User?> GetUserByIdAsync(string userId);
    public Task<User> AddUserAsync(User user);
    public Task<User> UpdateUserAsync(User user);
    public Task DeleteUserAsync(User user);
}
