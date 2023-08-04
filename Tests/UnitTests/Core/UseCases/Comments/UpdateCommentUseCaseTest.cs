using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.UseCases.Comments.UpdateComment;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Core.UseCases.Comments;

public class UpdateCommentUseCaseShould : MockDataSetup
{
    private readonly UpdateCommentUseCase _useCase;
    private readonly Mock<ICommentRepository> _mockCommentRepo;

    public UpdateCommentUseCaseShould()
    {
        _mockCommentRepo = new Mock<ICommentRepository>();
        _useCase = new UpdateCommentUseCase(_mockCommentRepo.Object);
    }

    [Fact]
    public async void UpdateComment_ReturnSuccess()
    {
        //Arrange
        CommentModel request = new()
        {
            Id = 1,
            SenderId = "19df82ba-a964-4dbf-8013-69c120e938de",
            Message = "Anotha message"
        };
        var moqComments = CreateMockCommentList();

        _mockCommentRepo
            .Setup(x => x.GetCommentByIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqComments.FirstOrDefault(z => z.Id == y)));

        _mockCommentRepo
            .Setup(x => x.UpdateCommentAsync(It.IsAny<Comment>()))
            .Returns((Comment y) => Task.FromResult(y));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<UpdateCommentResponse.Success>(result);
        if (result is UpdateCommentResponse.Success success)
        {
            Assert.Equal(request.Id, success.UpdatedComment.Id);
            Assert.Equal(request.SenderId, success.UpdatedComment.SenderId);
            Assert.Equal(request.Message, success.UpdatedComment.Message);
        }
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(74)]
    public async void UpdateNonexistentComment_ReturnCommentNotFound(int commentId)
    {
        //Arrange
        CommentModel request = new()
        {
            Id = commentId,
            SenderId = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3",
            Message = "WEEWOO"
        };
        var moqComments = CreateMockCommentList();

        _mockCommentRepo
            .Setup(x => x.GetCommentByIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqComments.FirstOrDefault(z => z.Id == y)));

        _mockCommentRepo
            .Setup(x => x.UpdateCommentAsync(It.IsAny<Comment>()))
            .Returns((Comment y) => Task.FromResult(y));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<UpdateCommentResponse.CommentNotFound>(result);
        Assert.Equal("A Comment with the given CommentId was not found", result.Message);
    }
}
