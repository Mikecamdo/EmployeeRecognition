using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Converters;
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

    public async Task<IEnumerable<CommentModel>> ExecuteAsync()
    {
        return CommentModelConverter.ToModel(await _commentRepository.GetCommentsAsync());
    }
}
