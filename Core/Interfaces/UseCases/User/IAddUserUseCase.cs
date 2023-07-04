using EmployeeRecognition.Database;

namespace EmployeeRecognition.Core.Interfaces.UseCases.User;

public interface IAddUserUseCase
{
    Task<User> ExecuteAsync(User user);
}
