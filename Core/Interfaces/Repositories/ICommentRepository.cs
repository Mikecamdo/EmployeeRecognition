using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.Repositories;

public interface ICommentRepository
{
    public Task<IEnumerable<Comment>> GetCommentsAsync();
    public Task<IEnumerable<Comment>> GetCommentsByKudoIdAsync(int kudoId);
    public Task<Comment> AddCommentAsync(Comment comment);
}
