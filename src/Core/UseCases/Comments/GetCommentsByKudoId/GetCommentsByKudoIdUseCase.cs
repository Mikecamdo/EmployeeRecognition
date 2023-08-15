using Laudatio.Core.Converters;
using Laudatio.Core.Entities;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Comments;

namespace Laudatio.Core.UseCases.Comments.GetCommentsByKudoId;

public class GetCommentsByKudoIdUseCase : IGetCommentsByKudoIdUseCase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IKudoRepository _kudoRepository;

    public GetCommentsByKudoIdUseCase(ICommentRepository commentRepository, IKudoRepository kudoRepository)
    {
        _commentRepository = commentRepository;
        _kudoRepository = kudoRepository;
    }

    public async Task<GetCommentsByKudoIdResponse> ExecuteAsync(int kudoId)
    {
        var kudo = await _kudoRepository.GetKudoByIdAsync(kudoId);

        if (kudo == null)
        {
            return new GetCommentsByKudoIdResponse.KudoNotFound();
        }

        var comments = await _commentRepository.GetCommentsByKudoIdAsync(kudoId);
        return new GetCommentsByKudoIdResponse.Success(CommentModelConverter.ToModel(comments));
    }
}
