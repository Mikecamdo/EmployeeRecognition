using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.UseCases.Users.AddUser;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IAddUserUseCase
{
    Task<AddUserResponse> ExecuteAsync(UserModel user);
}
