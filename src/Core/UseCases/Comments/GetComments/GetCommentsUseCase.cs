using Laudatio.Api.Models;
using Laudatio.Core.Converters;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Comments;

namespace Laudatio.Core.UseCases.Comments.GetComments;

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
