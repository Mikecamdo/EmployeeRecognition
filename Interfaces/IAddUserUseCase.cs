using EmployeeRecognition.Database;

namespace EmployeeRecognition.Interfaces;

public interface IAddUserUseCase
{
    Task<User> ExecuteAsync(User user);
}
