using Laudatio.Api.Models;
using Laudatio.Core.Entities;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.UseCases.Comments.AddComment;
using Moq;

namespace Laudatio.Tests.UnitTests.Core.UseCases.Comments;

public class AddCommentUseCaseShould : MockDataSetup
{
    private readonly AddCommentUseCase _useCase;
    private readonly Mock<ICommentRepository> _mockCommentRepo;
    private readonly Mock<IKudoRepository> _mockKudoRepo;

    public AddCommentUseCaseShould()
    {
        _mockCommentRepo = new Mock<ICommentRepository>();
        _mockKudoRepo = new Mock<IKudoRepository>();
        _useCase = new AddCommentUseCase(_mockCommentRepo.Object, _mockKudoRepo.Object);
    }

    [Fact]
    public async void AddComment_ReturnSuccess()
    {
        //Arrange
        var request = new CommentModel()
        {
            KudoId = 1,
            SenderId = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3",
            Message = "Test"
        };
        var moqKudos = CreateMockKudoList();

        _mockKudoRepo
            .Setup(x => x.GetKudoByIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqKudos.FirstOrDefault(z => z.Id == y)));

        _mockCommentRepo
            .Setup(x => x.AddCommentAsync(It.IsAny<Comment>()))
            .Returns((Comment y) => Task.FromResult(y));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<AddCommentResponse.Success>(result);
        if (result is AddCommentResponse.Success success)
        {
            Assert.Equal(request.KudoId, success.NewComment.KudoId);
            Assert.Equal(request.SenderId, success.NewComment.SenderId);
            Assert.Equal(request.Message, success.NewComment.Message);
        }
    }

    [Theory]
    [InlineData("", "A message")]
    [InlineData("id", "")]
    [InlineData("", "")]
    [InlineData(null, "A message")]
    public async void AddCommentWithMissingParameters_ReturnInvalidRequest(string senderId, string message)
    {
        //Arrange
        var request = new CommentModel()
        {
            KudoId = 1,
            SenderId = senderId,
            Message = message
        };
        var moqKudos = CreateMockKudoList();

        _mockKudoRepo
            .Setup(x => x.GetKudoByIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqKudos.FirstOrDefault(z => z.Id == y)));

        _mockCommentRepo
            .Setup(x => x.AddCommentAsync(It.IsAny<Comment>()))
            .Returns((Comment y) => Task.FromResult(y));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<AddCommentResponse.InvalidRequest>(result);
        Assert.Equal("Missing/invalid parameters", result.Message);
    }

    [Theory]
    [InlineData(-23)]
    [InlineData(1001)]
    public async void AddCommentWithNonexistentKudo_ReturnKudoNotFound(int kudoId)
    {
        //Arrange
        var request = new CommentModel()
        {
            KudoId = kudoId,
            SenderId = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3",
            Message = "A message"
        };
        var moqKudos = CreateMockKudoList();

        _mockKudoRepo
            .Setup(x => x.GetKudoByIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqKudos.FirstOrDefault(z => z.Id == y)));

        _mockCommentRepo
            .Setup(x => x.AddCommentAsync(It.IsAny<Comment>()))
            .Returns((Comment y) => Task.FromResult(y));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<AddCommentResponse.KudoNotFound>(result);
        Assert.Equal("A Kudo with the given KudoId was not found", result.Message);
    }
}
