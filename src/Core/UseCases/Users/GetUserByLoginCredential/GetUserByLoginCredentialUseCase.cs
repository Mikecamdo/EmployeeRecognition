using Laudatio.Api.Models;
using Laudatio.Core.Entities;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Users;

namespace Laudatio.Core.UseCases.Users.GetUserByLoginCredential;

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
