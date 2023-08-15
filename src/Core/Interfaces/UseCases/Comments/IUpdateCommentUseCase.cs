using Laudatio.Api.Models;
using Laudatio.Core.UseCases.Comments.UpdateComment;

namespace Laudatio.Core.Interfaces.UseCases.Comments;

public interface IUpdateCommentUseCase
{
    Task<UpdateCommentResponse> ExecuteAsync(CommentModel updatedCommentInfo);
}
