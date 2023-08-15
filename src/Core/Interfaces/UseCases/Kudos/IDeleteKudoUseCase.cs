using Laudatio.Core.UseCases.Kudos.DeleteKudo;

namespace Laudatio.Core.Interfaces.UseCases.Kudos;

public interface IDeleteKudoUseCase
{
    Task<DeleteKudoResponse> ExecuteAsync(int kudoId);
}
