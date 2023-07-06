using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

namespace EmployeeRecognition.Core.UseCases.Kudos;

public class AddKudoUseCase : IAddKudoUseCase
{
    private readonly IKudoRepository _kudoRepository;

    public AddKudoUseCase(IKudoRepository kudoRepository)
    {
        _kudoRepository = kudoRepository;
    }

    public async Task<Kudo> ExecuteAsync(Kudo kudo)
    {
        return await _kudoRepository.AddKudoAsync(kudo);
    }
}
