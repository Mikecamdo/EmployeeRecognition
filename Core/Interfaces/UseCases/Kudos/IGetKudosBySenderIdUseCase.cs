using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

public interface IGetKudosBySenderIdUseCase
{
    Task<IEnumerable<Kudo>> ExecuteAsync(string senderId);
}
