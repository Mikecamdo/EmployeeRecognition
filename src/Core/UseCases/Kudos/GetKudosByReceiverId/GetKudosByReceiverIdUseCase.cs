using Laudatio.Core.Converters;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Kudos;

namespace Laudatio.Core.UseCases.Kudos.GetKudosByReceiverId;

public class GetKudosByReceiverIdUseCase : IGetKudosByReceiverIdUseCase
{
    private readonly IKudoRepository _kudoRepository;
    private readonly IUserRepository _userRepository;

    public GetKudosByReceiverIdUseCase(IKudoRepository kudoRepository, IUserRepository userRepository)
    {
        _kudoRepository = kudoRepository;
        _userRepository = userRepository;
    }
    public async Task<GetKudosByReceiverIdResponse> ExecuteAsync(string receiverId)
    {
        var receiver = await _userRepository.GetUserByIdAsync(receiverId);

        if (receiver == null)
        {
            return new GetKudosByReceiverIdResponse.UserNotFound();
        }

        var receiverKudos = await _kudoRepository.GetKudosByReceiverIdAsync(receiverId);
        return new GetKudosByReceiverIdResponse.Success(KudoModelConverter.ToModel(receiverKudos));
    }
}
