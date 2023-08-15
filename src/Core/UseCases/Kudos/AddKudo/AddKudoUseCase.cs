using Laudatio.Api.Models;
using Laudatio.Core.Converters;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Kudos;

namespace Laudatio.Core.UseCases.Kudos.AddKudo;

public class AddKudoUseCase : IAddKudoUseCase
{
    private readonly IKudoRepository _kudoRepository;
    private readonly IUserRepository _userRepository;

    public AddKudoUseCase(IKudoRepository kudoRepository, IUserRepository userRepository)
    {
        _kudoRepository = kudoRepository;
        _userRepository = userRepository;
    }

    public async Task<AddKudoResponse> ExecuteAsync(KudoModel kudo)
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

        var newKudo = await _kudoRepository.AddKudoAsync(KudoModelConverter.ToEntity(kudo));
        return new AddKudoResponse.Success(KudoModelConverter.ToModel(newKudo));
    }
}
