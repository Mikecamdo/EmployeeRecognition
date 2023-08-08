using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.UseCases.Comments.UpdateComment;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Comments;

public interface IUpdateCommentUseCase
{
    Task<UpdateCommentResponse> ExecuteAsync(CommentModel updatedCommentInfo);
}
