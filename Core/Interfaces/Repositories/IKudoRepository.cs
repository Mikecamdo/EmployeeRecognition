using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.Repositories;

public interface IKudoRepository
{
    public Task<IEnumerable<Kudo>> GetAllKudosAsync();
    public Task<IEnumerable<Kudo>> GetKudosBySenderId(string senderId);
    public Task<IEnumerable<Kudo>> GetKudosByReceiverId(string receiverId);
    public Task<Kudo> AddKudoAsync(Kudo kudo);
}
