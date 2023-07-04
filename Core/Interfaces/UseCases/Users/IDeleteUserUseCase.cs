namespace EmployeeRecognition.Core.Interfaces.UseCases.Users;

public interface IDeleteUserUseCase
{
    Task ExecuteAsync(string userId);
}
