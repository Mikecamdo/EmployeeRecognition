using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.UseCases.Kudos.GetKudosBySenderId;
using Moq;

namespace Laudatio.Tests.UnitTests.Core.UseCases.Kudos;

public class GetKudosBySenderIdUseCaseShould : MockDataSetup
{
    private readonly GetKudosBySenderIdUseCase _useCase;
    private readonly Mock<IKudoRepository> _mockKudoRepo;
    private readonly Mock<IUserRepository> _mockUserRepo;

    public GetKudosBySenderIdUseCaseShould()
    {
        _mockKudoRepo = new Mock<IKudoRepository>();
        _mockUserRepo = new Mock<IUserRepository>();
        _useCase = new GetKudosBySenderIdUseCase(_mockKudoRepo.Object, _mockUserRepo.Object);
    }

    [Fact]
    public async void GetKudosBySenderId_ReturnSuccess()
    {
        //Arrange
        string request = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3";

        var moqKudos = CreateMockKudoList();
        var moqUsers = CreateMockUserList();

        _mockKudoRepo
            .Setup(x => x.GetKudosBySenderIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqKudos.Where(z => z.SenderId == y).ToList().AsEnumerable()));

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqUsers.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<GetKudosBySenderIdResponse.Success>(result);
        if (result is GetKudosBySenderIdResponse.Success success)
        {
            foreach (var kudo in success.SenderKudos)
            {
                Assert.Equal(request, kudo.SenderId);
            }
        }
    }

    [Theory]
    [InlineData("id")]
    [InlineData("id2")]
    public async void GetKudosByNonexistentSender_ReturnUserNotFound(string senderId)
    {
        //Arrange
        string request = senderId;

        var moqKudos = CreateMockKudoList();
        var moqUsers = CreateMockUserList();

        _mockKudoRepo
            .Setup(x => x.GetKudosBySenderIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqKudos.Where(z => z.SenderId == y).ToList().AsEnumerable()));

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqUsers.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<GetKudosBySenderIdResponse.UserNotFound>(result);
        Assert.Equal("A User with the given SenderId was not found", result.Message);
    }
}
