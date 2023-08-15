using Laudatio.Api.Models;
using Laudatio.Core.UseCases.Kudos.AddKudo;

namespace Laudatio.Core.Interfaces.UseCases.Kudos;

public interface IAddKudoUseCase
{
    Task<AddKudoResponse> ExecuteAsync(KudoModel kudo);
}
