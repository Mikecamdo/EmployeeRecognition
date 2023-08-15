using Laudatio.Core.Entities;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Laudatio.Core.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly MySqlDbContext _mySqlDbContext;

    public CommentRepository(MySqlDbContext mySqlDbContext)
    {
        _mySqlDbContext = mySqlDbContext;
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync()
    {
        return await _mySqlDbContext.Comments
            .Include(k => k.Kudo)
            .Include(u => u.Sender)
            .ToListAsync();
    }

    public async Task<Comment?> GetCommentByIdAsync(int commentId)
    {
        return await _mySqlDbContext.Comments
            .Include(k => k.Kudo)
            .Include(u => u.Sender)
            .FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public async Task<IEnumerable<Comment>> GetCommentsByKudoIdAsync(int kudoId)
    {
        return await _mySqlDbContext.Comments
            .Include(k => k.Kudo)
            .Include(u => u.Sender)
            .Where(c => c.KudoId == kudoId)
            .ToListAsync();
    }

    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        var kudo = await _mySqlDbContext.Kudos
            .FirstOrDefaultAsync(k => k.Id == comment.KudoId);

        var sender = await _mySqlDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == comment.SenderId);

        _mySqlDbContext.Comments.Add(comment);
        await _mySqlDbContext.SaveChangesAsync();
        comment.Kudo = kudo;
        comment.Sender = sender;
        return comment;
    }

    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        _mySqlDbContext.Comments.Update(comment);
        await _mySqlDbContext.SaveChangesAsync();
        return comment;
    }

    public async Task DeleteCommentAsync(Comment comment)
    {
        _mySqlDbContext.Comments.Remove(comment);
        await _mySqlDbContext.SaveChangesAsync();
    }
}
