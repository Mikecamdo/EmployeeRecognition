using Laudatio.Core.UseCases.Kudos.GetKudosBySenderId;

namespace Laudatio.Core.Interfaces.UseCases.Kudos;

public interface IGetKudosBySenderIdUseCase
{
    Task<GetKudosBySenderIdResponse> ExecuteAsync(string senderId);
}
