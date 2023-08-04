using EmployeeRecognition.Api.Converters;
using EmployeeRecognition.Api.Dto;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Comments;

namespace EmployeeRecognition.Core.UseCases.Comments.UpdateComment;

public class UpdateCommentUseCase : IUpdateCommentUseCase
{
    private readonly ICommentRepository _commentRepository;

    public UpdateCommentUseCase(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<UpdateCommentResponse> ExecuteAsync(int commentId, CommentDto updatedCommentInfo)
    {
        var toBeUpdated = await _commentRepository.GetCommentByIdAsync(commentId);

        if (toBeUpdated == null)
        {
            return new UpdateCommentResponse.CommentNotFound();
        }

        toBeUpdated.Message = updatedCommentInfo.Message;

        var updatedComment = await _commentRepository.UpdateCommentAsync(toBeUpdated);
        return new UpdateCommentResponse.Success(CommentModelConverter.ToModel(updatedComment));
    }
}
