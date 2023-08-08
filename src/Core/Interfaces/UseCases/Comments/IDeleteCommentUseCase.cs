using EmployeeRecognition.Core.UseCases.Comments.DeleteComment;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Comments;

public interface IDeleteCommentUseCase
{
    Task<DeleteCommentResponse> ExecuteAsync(int commentId);
}
