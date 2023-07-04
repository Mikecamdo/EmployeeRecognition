using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IUpdateUserUseCase
{
    Task<User> ExecuteAsync(User user);
}
