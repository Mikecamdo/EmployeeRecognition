using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Database.Context;

namespace EmployeeRecognition.Core.Repositories;

public class KudoRepository : IKudoRepository
{
    private readonly MySqlDbContext _mySqlDbContext;

    public KudoRepository(MySqlDbContext mySqlDbContext)
    {
        _mySqlDbContext = mySqlDbContext;
    }
    public async Task<Kudo> AddKudoAsync(Kudo kudo)
    {
        _mySqlDbContext.Kudos.Add(kudo);
        await _mySqlDbContext.SaveChangesAsync();
        return kudo;
    }
}
