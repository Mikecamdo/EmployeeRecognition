using Laudatio.Core.Entities;

namespace Laudatio.Core.Interfaces.Repositories;

public interface IKudoRepository
{
    public Task<IEnumerable<Kudo>> GetAllKudosAsync();
    public Task<Kudo?> GetKudoByIdAsync(int kudoId);
    public Task<IEnumerable<Kudo>> GetKudosBySenderIdAsync(string senderId);
    public Task<IEnumerable<Kudo>> GetKudosByReceiverIdAsync(string receiverId);
    public Task<Kudo> AddKudoAsync(Kudo kudo);
    public Task DeleteKudoAsync(Kudo kudo);
}
