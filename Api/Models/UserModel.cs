namespace EmployeeRecognition.Api.Models;

public class UserModel
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
    public required string AvatarUrl { get; set; }
}
