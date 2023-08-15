using Laudatio.Core.Converters;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Users;

namespace Laudatio.Core.UseCases.Users.GetUserByName;

public class GetUserByNameUseCase : IGetUserByNameUseCase
{
    private readonly IUserRepository _userRepository;
    public GetUserByNameUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByNameResponse> ExecuteAsync(string name)
    {
        var currentUser = await _userRepository.GetUserByNameAsync(name);

        if (currentUser == null)
        {
            return new GetUserByNameResponse.UserNotFound();
        }

        return new GetUserByNameResponse.Success(UserModelConverter.ToModel(currentUser));
    }
}
