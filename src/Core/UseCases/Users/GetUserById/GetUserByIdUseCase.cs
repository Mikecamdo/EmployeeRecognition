using Laudatio.Core.Converters;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Users;

namespace Laudatio.Core.UseCases.Users.GetUserById;

public class GetUserByIdUseCase : IGetUserByIdUseCase
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByIdResponse> ExecuteAsync(string userId)
    {
        var currentUser = await _userRepository.GetUserByIdAsync(userId);

        if (currentUser == null)
        {
            return new GetUserByIdResponse.UserNotFound();
        }

        return new GetUserByIdResponse.Success(UserModelConverter.ToModel(currentUser));
    }
}
