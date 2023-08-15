using Laudatio.Api.Models;

namespace Laudatio.Core.Interfaces.UseCases.Kudos;

public interface IGetAllKudosUseCase
{
    Task<IEnumerable<KudoModel>> ExecuteAsync();
}
