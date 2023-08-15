using Laudatio.Api.Models;
using Laudatio.Core.Converters;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Comments;

namespace Laudatio.Core.UseCases.Comments.UpdateComment;

public class UpdateCommentUseCase : IUpdateCommentUseCase
{
    private readonly ICommentRepository _commentRepository;

    public UpdateCommentUseCase(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<UpdateCommentResponse> ExecuteAsync(CommentModel updatedCommentInfo)
    {
        var toBeUpdated = await _commentRepository.GetCommentByIdAsync(updatedCommentInfo.Id);

        if (toBeUpdated == null)
        {
            return new UpdateCommentResponse.CommentNotFound();
        }

        toBeUpdated.Message = updatedCommentInfo.Message;

        var updatedComment = await _commentRepository.UpdateCommentAsync(toBeUpdated);
        return new UpdateCommentResponse.Success(CommentModelConverter.ToModel(updatedComment));
    }
}
