using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Comments;

namespace EmployeeRecognition.Core.UseCases.Comments.GetComments;

public class GetCommentsUseCase : IGetCommentsUseCase
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsUseCase(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<Comment>> ExecuteAsync()
    {
        return await _commentRepository.GetCommentsAsync();
    }
}
