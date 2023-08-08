using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.UseCases.Comments.GetComments;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Core.UseCases.Comments;

public class GetCommentsUseCaseShould : MockDataSetup
{
    private readonly GetCommentsUseCase _useCase;
    private readonly Mock<ICommentRepository> _mockCommentRepo;

    public GetCommentsUseCaseShould()
    {
        _mockCommentRepo = new Mock<ICommentRepository>();
        _useCase = new GetCommentsUseCase(_mockCommentRepo.Object);
    }

    [Fact]
    public async void GetComments_ReturnComments()
    {
        //Arrange
        var moqComments = CreateMockCommentList();

        _mockCommentRepo
            .Setup(x => x.GetCommentsAsync())
            .Returns(Task.FromResult(moqComments));

        //Act
        IEnumerable<CommentModel> result = await _useCase.ExecuteAsync();

        //Assert
        foreach(var comment in result)
        {
            Assert.IsType<CommentModel>(comment);
        }
    }
}
