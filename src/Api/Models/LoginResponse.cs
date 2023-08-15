namespace Laudatio.Api.Models;

public class LoginResponse
{
    public bool IsLoginSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Token { get; set; }
}
