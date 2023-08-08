using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.UseCases.Kudos.DeleteKudo;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Core.UseCases.Kudos;

public class DeleteKudoUseCaseShould : MockDataSetup
{
    private readonly DeleteKudoUseCase _useCase;
    private readonly Mock<IKudoRepository> _mockKudoRepo;

    public DeleteKudoUseCaseShould()
    {
        _mockKudoRepo = new Mock<IKudoRepository>();
        _useCase = new DeleteKudoUseCase(_mockKudoRepo.Object);
    }

    [Fact]
    public async void DeleteKudo_ReturnSuccess()
    {
        //Arrange
        int request = 1;
        var moqKudos = CreateMockKudoList();

        _mockKudoRepo
            .Setup(x => x.GetKudoByIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqKudos.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<DeleteKudoResponse.Success>(result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1000)]
    public async void DeleteNonexistentKudo_ReturnKudoNotFound(int kudoId)
    {
        //Arrange
        int request = kudoId;
        var moqKudos = CreateMockKudoList();

        _mockKudoRepo
            .Setup(x => x.GetKudoByIdAsync(It.IsAny<int>()))
            .Returns((int y) => Task.FromResult(moqKudos.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<DeleteKudoResponse.KudoNotFound>(result);
        Assert.Equal("A Kudo with the given KudoId was not found", result.Message);
    }
}
