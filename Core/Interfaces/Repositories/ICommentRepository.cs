using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.Repositories;

public interface ICommentRepository
{
    public Task<IEnumerable<Comment>> GetCommentsAsync();
    public Task<Comment?> GetCommentByIdAsync(int commentId);
    public Task<IEnumerable<Comment>> GetCommentsByKudoIdAsync(int kudoId);
    public Task<Comment> AddCommentAsync(Comment comment);
    public Task DeleteCommentAsync(Comment comment);
}
