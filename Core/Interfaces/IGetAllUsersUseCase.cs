using EmployeeRecognition.Database;

namespace EmployeeRecognition.Core.Interfaces;

public interface IGetAllUsersUseCase
{
    Task<IEnumerable<User>> ExecuteAsync();
}
