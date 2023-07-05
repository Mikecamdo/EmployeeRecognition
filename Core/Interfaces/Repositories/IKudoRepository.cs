using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.Repositories;

public interface IKudoRepository
{
    public Task<Kudo> AddKudoAsync(Kudo kudo);
}
