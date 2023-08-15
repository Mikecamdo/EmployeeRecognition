using Laudatio.Api.Models;

namespace Laudatio.Core.UseCases.Kudos.AddKudo;

public record AddKudoResponse(string Message)
{
    public record InvalidRequest(string Message) : AddKudoResponse(Message);
    public record Success(KudoModel NewKudo) : AddKudoResponse("");
}
