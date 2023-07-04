using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Users;

namespace EmployeeRecognition.Core.UseCases.Users;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserRepository _userRepository;
    public DeleteUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task ExecuteAsync(string userId)
    {
        var allUsers = _userRepository.GetUsersAsync(); //FIXME need to implement GetUserByIdAsync
        User? toBeDeleted = null;

        foreach (var user in allUsers.Result)
        {
            if (user.Id == userId)
            {
                toBeDeleted= user;
                break;
            }
        }

        if (toBeDeleted == null)
        {
            return;
        }

        await _userRepository.DeleteUserAsync(toBeDeleted);
    }
}
