using Laudatio.Core.Interfaces.UseCases.Users;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Converters;
using Laudatio.Api.Models;

namespace Laudatio.Core.UseCases.Users.GetAllUsers;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;
    public GetAllUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<UserModel>> ExecuteAsync()
    {
        return UserModelConverter.ToModel(await _userRepository.GetUsersAsync());
    }
}
