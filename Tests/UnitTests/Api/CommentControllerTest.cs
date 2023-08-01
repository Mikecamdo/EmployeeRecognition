using EmployeeRecognition.Api.Converters;
using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.UseCases.Comments.AddComment;
using EmployeeRecognition.Core.UseCases.Comments.DeleteComment;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Api;

public class CommentControllerTest : CommentControllerSetup
{
    [Theory]
    [InlineData(true, true)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public void AddComment(bool validComment, bool kudoExists)
    {
        //Arrange
        if (validComment && kudoExists)
        {
            _addCommentUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<Comment>()))
                .Returns((Comment y) =>
                    Task.FromResult<AddCommentResponse>(new AddCommentResponse.Success(
                        CommentModelConverter.ToModel(CreateMockCommentList().First()))));
        } else if (validComment && !kudoExists)
        {
            _addCommentUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<Comment>()))
                .Returns((Comment y) =>
                    Task.FromResult<AddCommentResponse>(new AddCommentResponse.KudoNotFound()));
        } else
        {
            _addCommentUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<Comment>()))
                .Returns((Comment y) =>
                    Task.FromResult<AddCommentResponse>(new AddCommentResponse.InvalidRequest("Missing/invalid parameters")));
        }

        CommentDto request = new()
        {
            SenderId = "19df82ba-a964-4dbf-8013-69c120e938de",
            Message = "A message"
        };

        //Act
        var ctrl = CreateCommentController();
        var result = ctrl.AddComment(request).Result;

        //Assert
        if (validComment && kudoExists)
        {
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var returnValue = okResult.Value;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(CommentModel));
        }
        else if (validComment && !kudoExists)
        {
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();

            var returnValue = badRequestResult.Value;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(string));
            Assert.Equal("A Kudo with the given KudoId was not found", returnValue);
        }
        else
        {
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();

            var returnValue = badRequestResult.Value;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(string));
            Assert.Equal("Missing/invalid parameters", returnValue);
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void DeleteComment(bool commentExists)
    {
        //Arrange
        if (commentExists)
        {
            _deleteCommentUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<DeleteCommentResponse>(new DeleteCommentResponse.Success()));
        } else
        {
            _deleteCommentUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<DeleteCommentResponse>(new DeleteCommentResponse.CommentNotFound()));
        }
        int request = 1;

        //Act
        var ctrl = CreateCommentController();
        var result = ctrl.DeleteComment(request).Result;

        //Assert
        if (commentExists)
        {
            result.Should().BeOfType<NoContentResult>();

            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
        } else
        {
            result.Should().BeOfType<NotFoundObjectResult>();

            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();

            var returnValue = notFoundResult.Value as string;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(string));
            Assert.Equal("A Comment with the given CommentId was not found", returnValue);
        }
    }
}
