using Laudatio.Api.Models;

namespace Laudatio.Core.UseCases.Users.UpdateUser;

public record UpdateUserResponse(string Message)
{
    public record UserNotFound() : UpdateUserResponse("A User with the given UserId was not found");
    public record InvalidRequest(string Message) : UpdateUserResponse(Message);
    public record Success(UserModel UpdatedUser) : UpdateUserResponse("");

}
