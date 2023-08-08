using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.UseCases.Comments.DeleteComment;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Core.UseCases.Comments;

public class DeleteCommentUseCaseShould : MockDataSetup
{
    private readonly DeleteCommentUseCase _useCase;
    private readonly Mock<ICommentRepository> _mockCommentRepo;

    public DeleteCommentUseCaseShould()
    {
        _mockCommentRepo = new Mock<ICommentRepository>();
        _useCase = new DeleteCommentUseCase(_mockCommentRepo.Object);
    }

    [Fact]
    public async void DeleteComment_ReturnSuccess()
    {
        //Arrange
        int request = 1;
        var moqComments = CreateMockCommentList();

        _mockCommentRepo
            .Setup(x => x.GetCommentByIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqComments.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<DeleteCommentResponse.Success>(result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(999)]
    public async void DeleteNonexistentComment_ReturnCommentNotFound(int commentId)
    {
        //Arrange
        int request = commentId;
        var moqComments = CreateMockCommentList();

        _mockCommentRepo
            .Setup(x => x.GetCommentByIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqComments.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<DeleteCommentResponse.CommentNotFound>(result);
        Assert.Equal("A Comment with the given CommentId was not found", result.Message);
    }
}
