using Laudatio.Api.Models;

namespace Laudatio.Core.Interfaces.UseCases.Comments;

public interface IGetCommentsUseCase
{
    Task<IEnumerable<CommentModel>> ExecuteAsync();
}
