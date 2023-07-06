using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Comments;

namespace EmployeeRecognition.Core.UseCases.Comments;

public class DeleteCommentUseCase : IDeleteCommentUseCase
{

    private readonly ICommentRepository _commentRepository;

    public DeleteCommentUseCase(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task ExecuteAsync(int commentId)
    {
        var toBeDeleted = await _commentRepository.GetCommentByIdAsync(commentId);

        if (toBeDeleted == null)
        {
            return;
        }

        await _commentRepository.DeleteCommentAsync(toBeDeleted);
    }
}
