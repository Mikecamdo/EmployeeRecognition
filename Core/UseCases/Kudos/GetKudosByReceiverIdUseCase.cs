using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

namespace EmployeeRecognition.Core.UseCases.Kudos;

public class GetKudosByReceiverIdUseCase : IGetKudosByReceiverIdUseCase
{
    private readonly IKudoRepository _kudoRepository;
    public GetKudosByReceiverIdUseCase(IKudoRepository kudoRepository)
    {
        _kudoRepository = kudoRepository;
    }
    public async Task<IEnumerable<Kudo>> ExecuteAsync(string receiverId)
    {
        return await _kudoRepository.GetKudosByReceiverIdAsync(receiverId);
    }
}
