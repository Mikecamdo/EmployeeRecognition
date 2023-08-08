using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.UseCases.Comments.GetCommentsByKudoId;

public record GetCommentsByKudoIdResponse(string Message)
{
    public record KudoNotFound() : GetCommentsByKudoIdResponse("A Kudo with the given KudoId was not found");
    public record Success(IEnumerable<CommentModel> Comments) : GetCommentsByKudoIdResponse("");
}
