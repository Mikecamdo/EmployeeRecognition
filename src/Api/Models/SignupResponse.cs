namespace EmployeeRecognition.Api.Models;

public class SignupResponse
{
    public bool IsSignupSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Token { get; set; }
}
