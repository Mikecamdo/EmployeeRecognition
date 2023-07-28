﻿using EmployeeRecognition.Core.Entities;
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
        return await _mySqlDbContext.Kudos
            .Include(s => s.Sender)
            .Include(r => r.Receiver)
            .ToListAsync();
    }

    public async Task<Kudo?> GetKudoByIdAsync(int kudoId)
    {
        return await _mySqlDbContext.Kudos.FirstOrDefaultAsync(k => k.Id == kudoId);
    }

    public async Task<IEnumerable<Kudo>> GetKudosBySenderIdAsync(string senderId)
    {
        return await _mySqlDbContext.Kudos
            .Where(k => k.SenderId == senderId)
            .Include(s => s.Sender)
            .Include(r => r.Receiver)
            .ToListAsync();
    }

    public async Task<IEnumerable<Kudo>> GetKudosByReceiverIdAsync(string receiverId)
    {
        return await _mySqlDbContext.Kudos
            .Where(k => k.ReceiverId == receiverId)
            .Include(s => s.Sender)
            .Include(r => r.Receiver)
            .ToListAsync();
    }

    public async Task<Kudo> AddKudoAsync(Kudo kudo)
    {
        _mySqlDbContext.Kudos.Add(kudo);
        await _mySqlDbContext.SaveChangesAsync();
        return kudo;
    }

    public async Task DeleteKudoAsync(Kudo kudo)
    {
        _mySqlDbContext.Kudos.Remove(kudo);

        var kudoComments = await _mySqlDbContext.Comments
            .Where(c => c.KudoId == kudo.Id)
            .ToListAsync();

        foreach(var comment in kudoComments)
        {
            _mySqlDbContext.Comments.Remove(comment);
        }

        await _mySqlDbContext.SaveChangesAsync();
    }
}
