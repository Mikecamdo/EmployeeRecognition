using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Comments;

namespace EmployeeRecognition.Core.UseCases.Comments;

public class AddCommentUseCase : IAddCommentUseCase
{
    private readonly ICommentRepository _commentRepository;

    public AddCommentUseCase(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Comment> ExecuteAsync(Comment comment)
    {
        return await _commentRepository.AddCommentAsync(comment);
    }
}
