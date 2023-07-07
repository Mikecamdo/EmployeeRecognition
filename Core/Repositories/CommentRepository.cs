using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRecognition.Core.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly MySqlDbContext _mySqlDbContext;

    public CommentRepository(MySqlDbContext mySqlDbContext)
    {
        _mySqlDbContext = mySqlDbContext;
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync()
    {
        return await _mySqlDbContext.Comments.ToListAsync();
    }

    public async Task<Comment?> GetCommentByIdAsync(int commentId)
    {
        return await _mySqlDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public async Task<IEnumerable<Comment>> GetCommentsByKudoIdAsync(int kudoId)
    {
        return await _mySqlDbContext.Comments.Where(c => c.KudoId == kudoId).ToListAsync();
    }

    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        _mySqlDbContext.Comments.Add(comment);
        await _mySqlDbContext.SaveChangesAsync();
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
