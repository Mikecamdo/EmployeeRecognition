using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Converters;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

namespace EmployeeRecognition.Core.UseCases.Kudos.GetAllKudos;

public class GetAllKudosUseCase : IGetAllKudosUseCase
{
    private readonly IKudoRepository _kudoRepository;
    public GetAllKudosUseCase(IKudoRepository kudoRepository)
    {
        _kudoRepository = kudoRepository;
    }
    public async Task<IEnumerable<KudoModel>> ExecuteAsync()
    {
        return KudoModelConverter.ToModel(await _kudoRepository.GetAllKudosAsync());
    }
}
