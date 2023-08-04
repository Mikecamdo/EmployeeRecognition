using EmployeeRecognition.Core.Interfaces.UseCases.Users;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Converters;

namespace EmployeeRecognition.Core.UseCases.Users.AddUser;

public class AddUserUseCase : IAddUserUseCase
{
    private readonly IUserRepository _userRepository;
    public AddUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AddUserResponse> ExecuteAsync(User user)
    {
        var currentUser = await _userRepository.GetUserByNameAsync(user.Name);

        if (currentUser != null)
        {
            return new AddUserResponse.InvalidRequest("A user with that name already exists");
        }

        var newUser = await _userRepository.AddUserAsync(user);

        return new AddUserResponse.Success(UserModelConverter.ToModel(newUser));
    }
}
