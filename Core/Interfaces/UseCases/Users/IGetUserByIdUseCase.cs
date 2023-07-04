using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IGetUserByIdUseCase
{
    Task<User?> ExecuteAsync(string userId);
}
