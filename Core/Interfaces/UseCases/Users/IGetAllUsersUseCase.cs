using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IGetAllUsersUseCase
{
    Task<IEnumerable<User>> ExecuteAsync();
}
