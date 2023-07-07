using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Comments;

public interface IUpdateCommentUseCase
{
    Task<Comment?> ExecuteAsync(int commentId, CommentDto updatedCommentInfo);
}
