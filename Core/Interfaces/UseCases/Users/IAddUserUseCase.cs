using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.UseCases.Users.AddUser;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IAddUserUseCase
{
    Task<AddUserResponse> ExecuteAsync(User user);
}
