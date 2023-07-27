using EmployeeRecognition.Core.UseCases.Users.DeleteUser;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IDeleteUserUseCase
{
    Task<DeleteUserResponse> ExecuteAsync(string userId);
}
