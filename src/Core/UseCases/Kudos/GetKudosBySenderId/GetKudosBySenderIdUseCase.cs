using Laudatio.Core.Converters;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Kudos;

namespace Laudatio.Core.UseCases.Kudos.GetKudosBySenderId;

public class GetKudosBySenderIdUseCase : IGetKudosBySenderIdUseCase
{
    private readonly IKudoRepository _kudoRepository;
    private readonly IUserRepository _userRepository;
    public GetKudosBySenderIdUseCase(IKudoRepository kudoRepository, IUserRepository userRepository)
    {
        _kudoRepository = kudoRepository;
        _userRepository = userRepository;
    }
    public async Task<GetKudosBySenderIdResponse> ExecuteAsync(string senderId)
    {
        var sender = await _userRepository.GetUserByIdAsync(senderId);

        if (sender == null) {
            return new GetKudosBySenderIdResponse.UserNotFound();
        }

        var senderKudos = await _kudoRepository.GetKudosBySenderIdAsync(senderId);
        return new GetKudosBySenderIdResponse.Success(KudoModelConverter.ToModel(senderKudos));
    }
}
