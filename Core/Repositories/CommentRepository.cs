using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Database.Context;

namespace EmployeeRecognition.Core.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly MySqlDbContext _mySqlDbContext;

    public CommentRepository(MySqlDbContext mySqlDbContext)
    {
        _mySqlDbContext = mySqlDbContext;
    }

    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        _mySqlDbContext.Comments.Add(comment);
        await _mySqlDbContext.SaveChangesAsync();
        return comment;
    }
}
