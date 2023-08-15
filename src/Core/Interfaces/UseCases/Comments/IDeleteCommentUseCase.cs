using Laudatio.Core.UseCases.Comments.DeleteComment;

namespace Laudatio.Core.Interfaces.UseCases.Comments;

public interface IDeleteCommentUseCase
{
    Task<DeleteCommentResponse> ExecuteAsync(int commentId);
}
