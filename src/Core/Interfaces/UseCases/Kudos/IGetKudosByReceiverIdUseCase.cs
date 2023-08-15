using Laudatio.Core.UseCases.Kudos.GetKudosByReceiverId;

namespace Laudatio.Core.Interfaces.UseCases.Kudos;

public interface IGetKudosByReceiverIdUseCase
{
    Task<GetKudosByReceiverIdResponse> ExecuteAsync(string receiverId);
}
