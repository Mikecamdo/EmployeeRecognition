using EmployeeRecognition.Core.Converters;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Users;

namespace EmployeeRecognition.Core.UseCases.Users.GetUserById;

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
