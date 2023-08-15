using Laudatio.Api.Models;
using Laudatio.Core.Converters;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Kudos;

namespace Laudatio.Core.UseCases.Kudos.GetAllKudos;

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
