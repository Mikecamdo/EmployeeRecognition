using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.UseCases.Users.GetUserByName;

public record GetUserByNameResponse(string Message)
{
    public record UserNotFound() : GetUserByNameResponse("A User with the given Name was not found");
    public record Success(UserModel User) : GetUserByNameResponse("");
}
