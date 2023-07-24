using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.UseCases.Kudos.AddKudo;

public record AddKudoResponse(string Message)
{
    public record InvalidRequest(string Message) : AddKudoResponse(Message);
    public record Success(KudoModel NewKudo) : AddKudoResponse("");
}
