using EmployeeRecognition.Core.Interfaces.UseCases.Users;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.UseCases.Users;

public class AddUserUseCase : IAddUserUseCase
{
    private readonly IUserRepository _userRepository;
    public AddUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> ExecuteAsync(User user)
    {
        return await _userRepository.AddUserAsync(user);
    }
}
