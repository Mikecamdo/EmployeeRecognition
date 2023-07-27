using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.UseCases.Users.AddUser;

public record AddUserResponse(string Message)
{
    public record InvalidRequest(string Message) : AddUserResponse(Message);
    public record Success(UserModel NewUser) : AddUserResponse("");
}
