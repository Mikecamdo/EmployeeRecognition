using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.UseCases.Kudos.AddKudo;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

public interface IAddKudoUseCase
{
    Task<AddKudoResponse> ExecuteAsync(Kudo kudo);
}
