using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Users;

namespace EmployeeRecognition.Core.UseCases.Users.GetUserByLoginCredential;

public class GetUserByLoginCredentialUseCase : IGetUserByLoginCredentialUseCase
{
    private readonly IUserRepository _userRepository;
    public GetUserByLoginCredentialUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User?> ExecuteAsync(LoginCredential loginCredential)
    {
        return await _userRepository.GetUserByLoginCredentialAsync(loginCredential);
    }
}
