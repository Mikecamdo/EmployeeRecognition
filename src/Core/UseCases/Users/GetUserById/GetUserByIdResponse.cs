using Laudatio.Api.Models;

namespace Laudatio.Core.UseCases.Users.GetUserById;

public record GetUserByIdResponse(string Message)
{
    public record UserNotFound() : GetUserByIdResponse("A User with the given UserId was not found");
    public record Success(UserModel User) : GetUserByIdResponse("");
}
