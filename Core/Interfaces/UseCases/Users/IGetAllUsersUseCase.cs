using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IGetAllUsersUseCase
{
    Task<IEnumerable<UserModel>> ExecuteAsync();
}
