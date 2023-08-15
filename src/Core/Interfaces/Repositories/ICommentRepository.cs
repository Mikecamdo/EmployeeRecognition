using Laudatio.Core.Entities;

namespace Laudatio.Core.Interfaces.Repositories;

public interface ICommentRepository
{
    public Task<IEnumerable<Comment>> GetCommentsAsync();
    public Task<Comment?> GetCommentByIdAsync(int commentId);
    public Task<IEnumerable<Comment>> GetCommentsByKudoIdAsync(int kudoId);
    public Task<Comment> AddCommentAsync(Comment comment);
    public Task<Comment> UpdateCommentAsync(Comment comment);
    public Task DeleteCommentAsync(Comment comment);
}
