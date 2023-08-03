using EmployeeRecognition.Api.Converters;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Kudos;

namespace EmployeeRecognition.Core.UseCases.Kudos.AddKudo;

public class AddKudoUseCase : IAddKudoUseCase
{
    private readonly IKudoRepository _kudoRepository;
    private readonly IUserRepository _userRepository;

    public AddKudoUseCase(IKudoRepository kudoRepository, IUserRepository userRepository)
    {
        _kudoRepository = kudoRepository;
        _userRepository = userRepository;
    }

    public async Task<AddKudoResponse> ExecuteAsync(Kudo kudo)
    {
        if (kudo.SenderId != "")
        {
            var sender = await _userRepository.GetUserByIdAsync(kudo.SenderId);

            if (sender == null)
            {
                return new AddKudoResponse.InvalidRequest("A User with the given SenderId was not found");
            }
        }

        var receiver = await _userRepository.GetUserByIdAsync(kudo.ReceiverId);

        if (receiver == null)
        {
            return new AddKudoResponse.InvalidRequest("A User with the given ReceiverId was not found");
        }

        if (kudo.SenderId == kudo.ReceiverId)
        {
            return new AddKudoResponse.InvalidRequest("Cannot send kudo to yourself");
        }

        var newKudo = await _kudoRepository.AddKudoAsync(kudo);
        return new AddKudoResponse.Success(KudoModelConverter.ToModel(newKudo));
    }
}
