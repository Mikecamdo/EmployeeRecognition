using EmployeeRecognition.Core.Interfaces.UseCases.Users;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Converters;
using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.UseCases.Users.AddUser;

public class AddUserUseCase : IAddUserUseCase
{
    private readonly IUserRepository _userRepository;
    public AddUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AddUserResponse> ExecuteAsync(UserModel user)
    {
        var currentUser = await _userRepository.GetUserByNameAsync(user.Name);

        if (currentUser != null)
        {
            return new AddUserResponse.InvalidRequest("A user with that name already exists");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var newUser = await _userRepository.AddUserAsync(UserModelConverter.ToEntity(user));

        return new AddUserResponse.Success(UserModelConverter.ToModel(newUser));
    }
}
