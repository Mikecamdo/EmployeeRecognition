using Laudatio.Api.Models;
using Laudatio.Core.UseCases.Comments.AddComment;

namespace Laudatio.Core.Interfaces.UseCases.Comments;

public interface IAddCommentUseCase
{
    Task<AddCommentResponse> ExecuteAsync(CommentModel comment);
}