using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<User> AddUserAsync(User user);
    public Task<IEnumerable<User>> GetUsersAsync();
}
