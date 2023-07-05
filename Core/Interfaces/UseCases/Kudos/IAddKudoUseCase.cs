using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

public interface IAddKudoUseCase
{
    Task<Kudo> ExecuteAsync(Kudo kudo);
}
