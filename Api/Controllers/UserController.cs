using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRecognition.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IGetAllUsersUseCase _getAllUsersUseCase;
    private readonly IGetUserByIdUseCase _getUserByIdUseCase;
    private readonly IAddUserUseCase _addUserUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;
    private readonly IDeleteUserUseCase _deleteUserUseCase;
    public UserController(
        IGetAllUsersUseCase getAllUsersUseCase,
        IGetUserByIdUseCase getUserByIdUseCase,
        IAddUserUseCase addUserUseCase,
        IUpdateUserUseCase updateUserUseCase,
        IDeleteUserUseCase deleteUserUseCase)
    {
        _getAllUsersUseCase = getAllUsersUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _addUserUseCase = addUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var allUsers = await _getAllUsersUseCase.ExecuteAsync();
        return Ok(allUsers);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById([FromRoute] string userId)
    {
        var currentUser = await _getUserByIdUseCase.ExecuteAsync(userId);
        if (currentUser == null)
        {
            return Ok();
        }
        return Ok(currentUser);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserDto user)
    {
        var userId = Guid.NewGuid().ToString();
        var newUser = new User() //FIXME need to move to a converter
        {
            Id = userId,
            Name = user.Name,
            Department = user.Department,
            AvatarUrl = user.AvatarUrl
        };
        var addedUser = await _addUserUseCase.ExecuteAsync(newUser);
        return Ok(addedUser);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser([FromRoute] string userId, [FromBody] UserDto updatedUserInfo)
    {
        var newUser = new User() //FIXME need to move to a converter
        {
            Id = userId,
            Name = updatedUserInfo.Name,
            Department = updatedUserInfo.Department,
            AvatarUrl = updatedUserInfo.AvatarUrl
        };
        var updatedUser = await _updateUserUseCase.ExecuteAsync(newUser);
        return Ok(updatedUser);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string userId)
    {
        await _deleteUserUseCase.ExecuteAsync(userId);
        return NoContent();
    }
}
