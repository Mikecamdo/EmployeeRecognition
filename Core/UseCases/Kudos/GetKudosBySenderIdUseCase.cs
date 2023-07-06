using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

namespace EmployeeRecognition.Core.UseCases.Kudos;

public class GetKudosBySenderIdUseCase : IGetKudosBySenderIdUseCase
{
    private readonly IKudoRepository _kudoRepository;
    public GetKudosBySenderIdUseCase(IKudoRepository kudoRepository)
    {
        _kudoRepository = kudoRepository;
    }
    public async Task<IEnumerable<Kudo>> ExecuteAsync(string senderId)
    {
        return await _kudoRepository.GetKudosBySenderId(senderId);
    }
}
