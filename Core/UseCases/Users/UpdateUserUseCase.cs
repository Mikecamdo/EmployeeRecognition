using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Users;

namespace EmployeeRecognition.Core.UseCases.Users;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    public UpdateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> ExecuteAsync(User user)
    {
        var toBeUpdated = await _userRepository.GetUserByIdAsync(user.Id);
        if (toBeUpdated == null)
        {
            return null;
        }

        return await _userRepository.UpdateUserAsync(user);
    }
}
