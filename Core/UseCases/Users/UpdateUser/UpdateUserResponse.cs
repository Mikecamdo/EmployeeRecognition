using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.UseCases.Users.UpdateUser;

public record UpdateUserResponse(string Message)
{
    public record UserNotFound() : UpdateUserResponse("A User with the given UserId was not found");
    public record Success(UserModel UpdatedUser) : UpdateUserResponse("");

}
