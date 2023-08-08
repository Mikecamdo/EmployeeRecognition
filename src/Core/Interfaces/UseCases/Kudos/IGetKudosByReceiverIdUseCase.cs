using EmployeeRecognition.Core.UseCases.Kudos.GetKudosByReceiverId;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

public interface IGetKudosByReceiverIdUseCase
{
    Task<GetKudosByReceiverIdResponse> ExecuteAsync(string receiverId);
}
