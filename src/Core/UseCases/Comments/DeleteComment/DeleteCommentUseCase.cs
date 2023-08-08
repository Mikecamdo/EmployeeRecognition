using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Comments;

namespace EmployeeRecognition.Core.UseCases.Comments.DeleteComment;

public class DeleteCommentUseCase : IDeleteCommentUseCase
{

    private readonly ICommentRepository _commentRepository;

    public DeleteCommentUseCase(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<DeleteCommentResponse> ExecuteAsync(int commentId)
    {
        var toBeDeleted = await _commentRepository.GetCommentByIdAsync(commentId);

        if (toBeDeleted == null)
        {
            return new DeleteCommentResponse.CommentNotFound();
        }

        await _commentRepository.DeleteCommentAsync(toBeDeleted);
        return new DeleteCommentResponse.Success();
    }
}
