using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Comments;

namespace EmployeeRecognition.Core.UseCases.Comments.GetCommentsByKudoId;

public class GetCommentsByKudoIdUseCase : IGetCommentsByKudoIdUseCase
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsByKudoIdUseCase(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<Comment>> ExecuteAsync(int kudoId)
    {
        return await _commentRepository.GetCommentsByKudoIdAsync(kudoId);
    }
}
