namespace EmployeeRecognition.Core.UseCases.Comments.DeleteComment;

public record DeleteCommentResponse(string Message)
{
    public record CommentNotFound() : DeleteCommentResponse("A Comment with the given CommentId was not found");
    public record Success() : DeleteCommentResponse("");

}
