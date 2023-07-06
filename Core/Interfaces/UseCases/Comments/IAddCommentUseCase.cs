using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Comments;

public interface IAddCommentUseCase
{
    Task<Comment> ExecuteAsync(Comment comment);
}