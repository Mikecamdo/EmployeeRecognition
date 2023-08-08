using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Users;

namespace EmployeeRecognition.Core.UseCases.Users.DeleteUser;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserRepository _userRepository;
    public DeleteUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<DeleteUserResponse> ExecuteAsync(string userId)
    {
        var toBeDeleted = await _userRepository.GetUserByIdAsync(userId);

        if (toBeDeleted == null)
        {
            return new DeleteUserResponse.UserNotFound();
        }

        await _userRepository.DeleteUserAsync(toBeDeleted);

        return new DeleteUserResponse.Success();
    }
}
