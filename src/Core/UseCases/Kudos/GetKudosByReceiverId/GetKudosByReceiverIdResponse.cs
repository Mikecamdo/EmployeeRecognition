using Laudatio.Api.Models;

namespace Laudatio.Core.UseCases.Kudos.GetKudosByReceiverId;

public record GetKudosByReceiverIdResponse(string Message)
{
    public record UserNotFound() : GetKudosByReceiverIdResponse("A User with the given ReceiverId was not found");
    public record Success(IEnumerable<KudoModel> ReceiverKudos) : GetKudosByReceiverIdResponse("");
}
