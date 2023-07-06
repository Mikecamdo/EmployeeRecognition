namespace EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

public interface IDeleteKudoUseCase
{
    Task ExecuteAsync(int kudoId);
}
