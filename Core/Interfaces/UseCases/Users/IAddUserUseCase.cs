using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IAddUserUseCase
{
    Task<User> ExecuteAsync(User user);
}
