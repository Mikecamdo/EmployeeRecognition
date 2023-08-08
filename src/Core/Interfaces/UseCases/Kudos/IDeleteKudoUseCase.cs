using EmployeeRecognition.Core.UseCases.Kudos.DeleteKudo;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

public interface IDeleteKudoUseCase
{
    Task<DeleteKudoResponse> ExecuteAsync(int kudoId);
}
