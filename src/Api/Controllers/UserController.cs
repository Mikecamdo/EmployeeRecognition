using System.IdentityModel.Tokens.Jwt;
using Laudatio.Api.Converters;
using Laudatio.Api.Dtos;
using Laudatio.Api.JwtFeatures;
using Laudatio.Api.Models;
using Laudatio.Core.Converters;
using Laudatio.Core.Interfaces.UseCases.Users;
using Laudatio.Core.UseCases.Users.AddUser;
using Laudatio.Core.UseCases.Users.DeleteUser;
using Laudatio.Core.UseCases.Users.GetUserById;
using Laudatio.Core.UseCases.Users.GetUserByName;
using Laudatio.Core.UseCases.Users.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laudatio.Api.Controllers;

[Route("users")]
public class UserController : ControllerBase
{
    private readonly JwtHandler _jwtHandler;

    private readonly IGetAllUsersUseCase _getAllUsersUseCase;
    private readonly IGetUserByIdUseCase _getUserByIdUseCase;
    private readonly IGetUserByNameUseCase _getUserByNameUseCase;
    private readonly IGetUserByLoginCredentialUseCase _getUserByLoginCredentialUseCase;
    private readonly IAddUserUseCase _addUserUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;
    private readonly IDeleteUserUseCase _deleteUserUseCase;

    public UserController(
        JwtHandler jwtHandler,
        IGetAllUsersUseCase getAllUsersUseCase,
        IGetUserByIdUseCase getUserByIdUseCase,
        IGetUserByNameUseCase getUserByNameUseCase,
        IGetUserByLoginCredentialUseCase getUserByLoginCredentialUseCase,
        IAddUserUseCase addUserUseCase,
        IUpdateUserUseCase updateUserUseCase,
        IDeleteUserUseCase deleteUserUseCase)
    {
        _jwtHandler = jwtHandler;

        _getAllUsersUseCase = getAllUsersUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _getUserByNameUseCase = getUserByNameUseCase;
        _getUserByLoginCredentialUseCase = getUserByLoginCredentialUseCase;
        _addUserUseCase = addUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserModel>))]
    public async Task<IActionResult> GetUsers()
    {
        var allUsers = await _getAllUsersUseCase.ExecuteAsync();
        return Ok(allUsers);
    }

    [Authorize]
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById([FromRoute] string userId)
    {
        var getUserByIdResponse = await _getUserByIdUseCase.ExecuteAsync(userId);

        return getUserByIdResponse switch
        {
            GetUserByIdResponse.Success success => Ok(success.User),
            GetUserByIdResponse.UserNotFound => NotFound(getUserByIdResponse.Message),
            _ => Problem("Unexpected response")
        };
    }

    [Authorize]
    [HttpGet("name")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserByName([FromQuery] string name)
    {
        var getUserByNameResponse = await _getUserByNameUseCase.ExecuteAsync(name);

        return getUserByNameResponse switch
        {
            GetUserByNameResponse.Success success => Ok(success.User),
            GetUserByNameResponse.UserNotFound => NotFound(getUserByNameResponse.Message),
            _ => Problem("Unexpected response")
        };
    }

    [HttpGet("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(LoginResponse))]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignupResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SignupResponse))]
    public async Task<IActionResult> AddUser([FromBody] UserDto user)
    {
        var newUser = UserDtoConverter.ToModel(user);
        var addUserResponse = await _addUserUseCase.ExecuteAsync(newUser);

        switch (addUserResponse)
        {
            case AddUserResponse.Success success:
                var signingCredentials = _jwtHandler.GetSigningCredentials();
                var claims = _jwtHandler.GetClaims(UserModelConverter.ToEntity(success.NewUser));
                var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                var signupResponse = new SignupResponse()
                {
                    IsSignupSuccessful = true,
                    Token = token
                };

                return Ok(signupResponse);
            case AddUserResponse.InvalidRequest:
                return BadRequest(new SignupResponse { IsSignupSuccessful = false, ErrorMessage = addUserResponse.Message });
            default:
                return Problem("Unexpected result");
        }
    }

    [Authorize]
    [HttpPut("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignupResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromRoute] string userId, [FromBody] UserDto updatedUserInfo)
    {
        var currentUser = UserDtoConverter.ToModel(updatedUserInfo);
        currentUser.Id = userId;
        var updateUserResponse = await _updateUserUseCase.ExecuteAsync(currentUser);

        switch (updateUserResponse)
        {
            case UpdateUserResponse.Success success:
                var signingCredentials = _jwtHandler.GetSigningCredentials();
                var claims = _jwtHandler.GetClaims(UserModelConverter.ToEntity(success.UpdatedUser));
                var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                var signupResponse = new SignupResponse()
                {
                    IsSignupSuccessful = true,
                    Token = token
                };

                return Ok(signupResponse);
            case UpdateUserResponse.InvalidRequest:
                return BadRequest(updateUserResponse.Message);
            case UpdateUserResponse.UserNotFound:
                return NotFound(updateUserResponse.Message);
            default:
                return Problem("Unexpected result");
        }
    }

    [Authorize]
    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] string userId)
    {
        var deleteUserResponse = await _deleteUserUseCase.ExecuteAsync(userId);

        return deleteUserResponse switch
        {
            DeleteUserResponse.Success => NoContent(),
            DeleteUserResponse.UserNotFound => NotFound(deleteUserResponse.Message),
            _ => Problem("Unexpected response")
        };
    }
}
