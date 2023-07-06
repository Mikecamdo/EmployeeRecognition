using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRecognition.Core.Repositories;

public class KudoRepository : IKudoRepository
{
    private readonly MySqlDbContext _mySqlDbContext;

    public KudoRepository(MySqlDbContext mySqlDbContext)
    {
        _mySqlDbContext = mySqlDbContext;
    }

    public async Task<IEnumerable<Kudo>> GetAllKudosAsync()
    {
        return await _mySqlDbContext.Kudos.ToListAsync();
    }
    public async Task<Kudo> AddKudoAsync(Kudo kudo)
    {
        _mySqlDbContext.Kudos.Add(kudo);
        await _mySqlDbContext.SaveChangesAsync();
        return kudo;
    }
}
