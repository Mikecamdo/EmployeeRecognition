using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

public interface IGetKudosByReceiverIdUseCase
{
    Task<IEnumerable<Kudo>> ExecuteAsync(string receiverId);
}
