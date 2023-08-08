using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.UseCases.Comments.AddComment;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Comments;

public interface IAddCommentUseCase
{
    Task<AddCommentResponse> ExecuteAsync(CommentModel comment);
}