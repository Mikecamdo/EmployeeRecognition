using EmployeeRecognition.Core.UseCases.Users.GetUserById;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IGetUserByIdUseCase
{
    Task<GetUserByIdResponse> ExecuteAsync(string userId);
}
