using EmployeeRecognition.Api.Dto;
using EmployeeRecognition.Core.UseCases.Comments.UpdateComment;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Comments;

public interface IUpdateCommentUseCase
{
    Task<UpdateCommentResponse> ExecuteAsync(int commentId, CommentDto updatedCommentInfo);
}
