using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.UseCases.Comments.GetCommentsByKudoId;
using Moq;

namespace Laudatio.Tests.UnitTests.Core.UseCases.Comments;

public class GetCommentsByKudoIdUseCaseShould : MockDataSetup
{
    private readonly GetCommentsByKudoIdUseCase _useCase;
    private readonly Mock<ICommentRepository> _mockCommentRepo;
    private readonly Mock<IKudoRepository> _mockKudoRepo;

    public GetCommentsByKudoIdUseCaseShould()
    {
        _mockCommentRepo = new Mock<ICommentRepository>();
        _mockKudoRepo = new Mock<IKudoRepository>();
        _useCase = new GetCommentsByKudoIdUseCase(_mockCommentRepo.Object, _mockKudoRepo.Object);
    }

    [Fact]
    public async void GetCommentsByKudoId_ReturnSuccess()
    {
        //Arrange
        int request = 1;
        var moqKudos = CreateMockKudoList();
        var moqComments = CreateMockCommentList();

        _mockKudoRepo
            .Setup(x => x.GetKudoByIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqKudos.FirstOrDefault(z => z.Id == y)));

        _mockCommentRepo
            .Setup(x => x.GetCommentsByKudoIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqComments.Where(z => z.KudoId == y).ToList().AsEnumerable()));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<GetCommentsByKudoIdResponse.Success>(result);
        if (result is GetCommentsByKudoIdResponse.Success success)
        {
            foreach (var comment in success.Comments)
            {
                Assert.Equal(request, comment.KudoId);
            }
        }
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(200)]
    public async void GetCommentsByNonexistentKudo_ReturnKudoNotFound(int kudoId)
    {
        //Arrange
        int request = kudoId;
        var moqKudos = CreateMockKudoList();
        var moqComments = CreateMockCommentList();

        _mockKudoRepo
            .Setup(x => x.GetKudoByIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqKudos.FirstOrDefault(z => z.Id == y)));

        _mockCommentRepo
            .Setup(x => x.GetCommentsByKudoIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqComments.Where(z => z.KudoId == y).ToList().AsEnumerable()));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<GetCommentsByKudoIdResponse.KudoNotFound>(result);
        Assert.Equal("A Kudo with the given KudoId was not found", result.Message);
    }
}
