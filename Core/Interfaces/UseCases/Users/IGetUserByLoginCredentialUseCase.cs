using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IGetUserByLoginCredentialUseCase
{
    Task<User?> ExecuteAsync(LoginCredential loginCredential);
}
