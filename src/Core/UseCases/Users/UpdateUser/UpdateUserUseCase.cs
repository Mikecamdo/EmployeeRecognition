using Laudatio.Api.Models;
using Laudatio.Core.Converters;
using Laudatio.Core.Entities;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Users;

namespace Laudatio.Core.UseCases.Users.UpdateUser;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    public UpdateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UpdateUserResponse> ExecuteAsync(UserModel updatedUserInfo)
    {
        var toBeUpdated = await _userRepository.GetUserByIdAsync(updatedUserInfo.Id);
        
        if (toBeUpdated == null)
        {
            return new UpdateUserResponse.UserNotFound();
        }

        var otherUser = await _userRepository.GetUserByNameAsync(updatedUserInfo.Name);

        if (otherUser != null && otherUser.Id != updatedUserInfo.Id)
        {
            return new UpdateUserResponse.InvalidRequest("Name already in use");
        }

        toBeUpdated.Name = updatedUserInfo.Name;
        if (updatedUserInfo.Password != null)
        {
            toBeUpdated.Password = BCrypt.Net.BCrypt.HashPassword(updatedUserInfo.Password);
        }
        toBeUpdated.AvatarUrl = updatedUserInfo.AvatarUrl;
        toBeUpdated.Bio = updatedUserInfo.Bio;

        var updatedUser = await _userRepository.UpdateUserAsync(toBeUpdated);
        return new UpdateUserResponse.Success(UserModelConverter.ToModel(updatedUser));
    }
}
