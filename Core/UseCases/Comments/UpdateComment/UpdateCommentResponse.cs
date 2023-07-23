using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.UseCases.Comments.UpdateComment;

public record UpdateCommentResponse(string Message)
{
    public record CommentNotFound() : UpdateCommentResponse("A Comment with the given CommentId was not found");
    public record Success(CommentModel UpdatedComment) : UpdateCommentResponse("");
}
