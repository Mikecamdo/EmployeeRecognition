using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.UseCases.Kudos.GetAllKudos;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Core.UseCases.Kudos;

public class GetAllKudosUseCaseShould : MockDataSetup
{
    private readonly GetAllKudosUseCase _useCase;
    private readonly Mock<IKudoRepository> _mockKudoRepo;

    public GetAllKudosUseCaseShould()
    {
        _mockKudoRepo = new Mock<IKudoRepository>();
        _useCase = new GetAllKudosUseCase(_mockKudoRepo.Object);
    }

    [Fact]
    public async void GetAllKudos_ReturnAllKudos()
    {
        //Arrange
        var moqKudos = CreateMockKudoList();

        _mockKudoRepo
            .Setup(x => x.GetAllKudosAsync())
            .Returns(Task.FromResult(moqKudos));

        //Act
        IEnumerable<Kudo> result = await _useCase.ExecuteAsync();

        //Assert
        foreach(var kudo in result)
        {
            Assert.IsType<Kudo>(kudo);
        }
    }
}
