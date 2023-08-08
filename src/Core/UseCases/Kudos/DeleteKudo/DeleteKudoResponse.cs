namespace EmployeeRecognition.Core.UseCases.Kudos.DeleteKudo;

public record DeleteKudoResponse(string Message)
{
    public record KudoNotFound() : DeleteKudoResponse("A Kudo with the given KudoId was not found");
    public record Success() : DeleteKudoResponse("");
}
