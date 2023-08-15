using Laudatio.Core.UseCases.Comments.GetCommentsByKudoId;

namespace Laudatio.Core.Interfaces.UseCases.Comments;

public interface IGetCommentsByKudoIdUseCase
{
    Task<GetCommentsByKudoIdResponse> ExecuteAsync(int kudoId);
}
