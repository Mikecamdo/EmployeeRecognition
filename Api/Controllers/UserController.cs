using System.IdentityModel.Tokens.Jwt;
using EmployeeRecognition.Api.JwtFeatures;
using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRecognition.Api.Controllers;

[Route("users")]
public class UserController : ControllerBase
{
    private readonly JwtHandler _jwtHandler;

    private readonly IGetAllUsersUseCase _getAllUsersUseCase;
    private readonly IGetUserByIdUseCase _getUserByIdUseCase;
    private readonly IGetUserByLoginCredentialUseCase _getUserByLoginCredentialUseCase;
    private readonly IAddUserUseCase _addUserUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;
    private readonly IDeleteUserUseCase _deleteUserUseCase;
    public UserController(
        JwtHandler jwtHandler,
        IGetAllUsersUseCase getAllUsersUseCase,
        IGetUserByIdUseCase getUserByIdUseCase,
        IGetUserByLoginCredentialUseCase getUserByLoginCredentialUseCase,
        IAddUserUseCase addUserUseCase,
        IUpdateUserUseCase updateUserUseCase,
        IDeleteUserUseCase deleteUserUseCase)
    {
        _jwtHandler = jwtHandler;

        _getAllUsersUseCase = getAllUsersUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _getUserByLoginCredentialUseCase = getUserByLoginCredentialUseCase;
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

    //FIXME would need to have user registration in the same controller as login (so its not behind an [Authorize] tag
    [HttpGet("login")] //FIXME need to move login to its own controller (so the rest of the users' HTTP request can be behind an [Authorize] tag)
    public async Task<IActionResult> GetUserByLoginCredentials([FromQuery] LoginCredential loginCredential)
    {
        var currentUser = await _getUserByLoginCredentialUseCase.ExecuteAsync(loginCredential);
        if (currentUser == null)
        {
            return Unauthorized(new LoginResponse { ErrorMessage = "Invalid Login" });
        }

        var signingCredentials = _jwtHandler.GetSigningCredentials();
        var claims = _jwtHandler.GetClaims(currentUser);
        var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        var loginResponse = new LoginResponse()
        {
            IsLoginSuccessful = true,
            Token = token
        };

        return Ok(loginResponse);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserDto user)
    {
        var userId = Guid.NewGuid().ToString();
        var newUser = new User() //FIXME need to move to a converter
        {
            Id = userId,
            Name = user.Name,
            Password = user.Password,
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
            Password = updatedUserInfo.Password,
            AvatarUrl = updatedUserInfo.AvatarUrl
        };
        var updatedUser = await _updateUserUseCase.ExecuteAsync(newUser);
        if (updatedUser == null)
        {
            return Ok(); //FIXME only return OK if doing what the user expects to happen
        }
        return Ok(updatedUser);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string userId)
    {
        await _deleteUserUseCase.ExecuteAsync(userId);
        return NoContent();
    }
}
