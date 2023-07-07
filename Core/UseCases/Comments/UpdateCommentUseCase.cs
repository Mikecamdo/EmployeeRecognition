using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Comments;

namespace EmployeeRecognition.Core.UseCases.Comments;

public class UpdateCommentUseCase : IUpdateCommentUseCase
{
    private readonly ICommentRepository _commentRepository;

    public UpdateCommentUseCase(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<Comment?> ExecuteAsync(int commentId, CommentDto updatedCommentInfo)
    {
        var toBeUpdated = await _commentRepository.GetCommentByIdAsync(commentId);

        if (toBeUpdated == null)
        {
            return null;
        }

        toBeUpdated.KudoId = updatedCommentInfo.KudoId;
        toBeUpdated.SenderId = updatedCommentInfo.SenderId;
        toBeUpdated.SenderName = updatedCommentInfo.SenderName;
        toBeUpdated.SenderAvatar = updatedCommentInfo.SenderAvatar;
        toBeUpdated.Message = updatedCommentInfo.Message;

        return await _commentRepository.UpdateCommentAsync(toBeUpdated);
    }
}
