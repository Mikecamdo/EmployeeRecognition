using EmployeeRecognition.Core.UseCases.Kudos.GetKudosBySenderId;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

public interface IGetKudosBySenderIdUseCase
{
    Task<GetKudosBySenderIdResponse> ExecuteAsync(string senderId);
}
