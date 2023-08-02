using EmployeeRecognition.Core.UseCases.Users.GetUserByName;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IGetUserByNameUseCase
{
    Task<GetUserByNameResponse> ExecuteAsync(string name);
}
