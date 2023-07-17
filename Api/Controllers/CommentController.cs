using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.UseCases.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRecognition.Api.Controllers;

[Authorize]
[Route("comment")]
public class CommentController : ControllerBase
{
    private readonly IGetCommentsUseCase _getCommentsUseCase;
    private readonly IGetCommentsByKudoIdUseCase _getCommentsByKudoIdUseCase;
    private readonly IAddCommentUseCase _addCommentUseCase;
    private readonly IUpdateCommentUseCase _updateCommentUseCase;
    private readonly IDeleteCommentUseCase _deleteCommentUseCase;

    public CommentController(
        IGetCommentsUseCase getCommentsUseCase,
        IGetCommentsByKudoIdUseCase getCommentsByKudoIdUseCase,
        IAddCommentUseCase addCommentUseCase,
        IUpdateCommentUseCase updateCommentUseCase,
        IDeleteCommentUseCase deleteCommentUseCase)
    {
        _getCommentsUseCase = getCommentsUseCase;
        _getCommentsByKudoIdUseCase = getCommentsByKudoIdUseCase;
        _addCommentUseCase = addCommentUseCase;
        _updateCommentUseCase = updateCommentUseCase;
        _deleteCommentUseCase = deleteCommentUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllComments()
    {
        var allComments = await _getCommentsUseCase.ExecuteAsync();
        return Ok(allComments);

    }

    [HttpGet("{kudoId}")]
    public async Task<IActionResult> GetCommentsByKudoId([FromRoute] int kudoId)
    {
        var kudoComments = await _getCommentsByKudoIdUseCase.ExecuteAsync(kudoId);
        return Ok(kudoComments);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] CommentDto comment)
    {
        var newComment = new Comment() //FIXME need to move to a converter
        {
            KudoId = comment.KudoId,
            SenderId = comment.SenderId,
            SenderName = comment.SenderName,
            SenderAvatar = comment.SenderAvatar,
            Message = comment.Message,
        };

        var addedComment = await _addCommentUseCase.ExecuteAsync(newComment);
        return Ok(addedComment);
    }

    [HttpPut("{commentId}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int commentId, [FromBody] CommentDto updatedCommentInfo)
    {
        var updatedComment = await _updateCommentUseCase.ExecuteAsync(commentId, updatedCommentInfo);
        if (updatedComment == null)
        {
            return Ok();
        }
        return Ok(updatedComment);
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        await _deleteCommentUseCase.ExecuteAsync(commentId);
        return NoContent();
    }
}
