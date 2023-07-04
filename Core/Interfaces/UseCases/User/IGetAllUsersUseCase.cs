using EmployeeRecognition.Database;

namespace EmployeeRecognition.Core.Interfaces.UseCases.User;

public interface IGetAllUsersUseCase
{
    Task<IEnumerable<User>> ExecuteAsync();
}
