using EmployeeRecognition.Database;
using EmployeeRecognition.Interfaces;
using EmployeeRecognition.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRecognition.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IAddUserUseCase _addUserUseCase;
    public UserController(IAddUserUseCase addUserUseCase) 
    {
        _addUserUseCase= addUserUseCase;
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
}
