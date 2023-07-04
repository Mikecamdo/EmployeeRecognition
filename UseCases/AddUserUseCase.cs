using EmployeeRecognition.Interfaces;
using EmployeeRecognition.Database;

namespace EmployeeRecognition.UseCases;

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
