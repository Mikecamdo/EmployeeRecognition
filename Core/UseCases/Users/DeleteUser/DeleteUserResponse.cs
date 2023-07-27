namespace EmployeeRecognition.Core.UseCases.Users.DeleteUser;

public record DeleteUserResponse(string Message)
{
    public record UserNotFound() : DeleteUserResponse("A User with the given UserId was not found");
    public record Success() : DeleteUserResponse("");
}
