using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.Repositories;

public interface ICommentRepository
{
    public Task<Comment> AddCommentAsync(Comment comment);
}
