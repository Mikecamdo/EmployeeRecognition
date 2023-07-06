using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

public interface IGetAllKudosUseCase
{
    Task<IEnumerable<Kudo>> ExecuteAsync();
}
