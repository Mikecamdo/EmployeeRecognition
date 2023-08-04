﻿using EmployeeRecognition.Api.Converters;
using EmployeeRecognition.Api.Dto;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Users;

namespace EmployeeRecognition.Core.UseCases.Users.UpdateUser;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    public UpdateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UpdateUserResponse> ExecuteAsync(string userId, UserDto updatedUserInfo)
    {
        var toBeUpdated = await _userRepository.GetUserByIdAsync(userId);
        
        if (toBeUpdated == null)
        {
            return new UpdateUserResponse.UserNotFound();
        }

        var otherUser = await _userRepository.GetUserByNameAsync(updatedUserInfo.Name);

        if (otherUser != null && otherUser.Id != userId)
        {
            return new UpdateUserResponse.InvalidRequest("Name already in use");
        }

        toBeUpdated.Name = updatedUserInfo.Name;
        if (updatedUserInfo.Password != null)
        {
            toBeUpdated.Password = updatedUserInfo.Password;
        }
        toBeUpdated.AvatarUrl = updatedUserInfo.AvatarUrl;
        toBeUpdated.Bio = updatedUserInfo.Bio;

        var updatedUser = await _userRepository.UpdateUserAsync(toBeUpdated);
        return new UpdateUserResponse.Success(UserModelConverter.ToModel(updatedUser));
    }
}
