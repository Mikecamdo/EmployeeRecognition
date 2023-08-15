using Laudatio.Api.Models;

namespace Laudatio.Core.UseCases.Comments.AddComment;

public record AddCommentResponse(string Message)
{
    public record InvalidRequest(string Message) : AddCommentResponse(Message);
    public record KudoNotFound() : AddCommentResponse("A Kudo with the given KudoId was not found");
    public record Success(CommentModel NewComment) : AddCommentResponse("");
}
