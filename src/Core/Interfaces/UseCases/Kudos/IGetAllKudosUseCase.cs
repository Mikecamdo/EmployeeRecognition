using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

public interface IGetAllKudosUseCase
{
    Task<IEnumerable<KudoModel>> ExecuteAsync();
}
