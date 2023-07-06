using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

namespace EmployeeRecognition.Core.UseCases.Kudos;

public class DeleteKudoUseCase : IDeleteKudoUseCase
{
    private readonly IKudoRepository _kudoRepository;
    public DeleteKudoUseCase(IKudoRepository kudoRepository)
    {
        _kudoRepository = kudoRepository;
    }
    public async Task ExecuteAsync(int kudoId)
    {
        var toBeDeleted = await _kudoRepository.GetKudoByIdAsync(kudoId);

        if (toBeDeleted == null)
        {
            return;
        }

        await _kudoRepository.DeleteKudoAsync(toBeDeleted);
    }
}
