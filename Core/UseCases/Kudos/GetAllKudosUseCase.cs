using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

namespace EmployeeRecognition.Core.UseCases.Kudos;

public class GetAllKudosUseCase : IGetAllKudosUseCase
{
    private readonly IKudoRepository _kudoRepository;
    public GetAllKudosUseCase(IKudoRepository kudoRepository)
    {
        _kudoRepository = kudoRepository;
    }
    public async Task<IEnumerable<Kudo>> ExecuteAsync()
    {
        return await _kudoRepository.GetAllKudosAsync();
    }
}
