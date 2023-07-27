using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.UseCases.Users.UpdateUser;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IUpdateUserUseCase
{
    Task<UpdateUserResponse> ExecuteAsync(string userId, UserDto updatedUserInfo);
}
