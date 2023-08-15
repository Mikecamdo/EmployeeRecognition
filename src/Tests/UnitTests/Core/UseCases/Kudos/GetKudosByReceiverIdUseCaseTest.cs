using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.UseCases.Kudos.GetKudosByReceiverId;
using Moq;

namespace Laudatio.Tests.UnitTests.Core.UseCases.Kudos;

public class GetKudosByReceiverIdUseCaseShould : MockDataSetup
{
    private readonly GetKudosByReceiverIdUseCase _useCase;
    private readonly Mock<IKudoRepository> _mockKudoRepo;
    private readonly Mock<IUserRepository> _mockUserRepo;

    public GetKudosByReceiverIdUseCaseShould()
    {
        _mockKudoRepo = new Mock<IKudoRepository>();
        _mockUserRepo = new Mock<IUserRepository>();
        _useCase = new GetKudosByReceiverIdUseCase(_mockKudoRepo.Object, _mockUserRepo.Object);
    }

    [Fact]
    public async void GetKudosByReceiverId_ReturnSuccess()
    {
        //Arrange
        string request = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3";

        var moqKudos = CreateMockKudoList();
        var moqUsers = CreateMockUserList();

        _mockKudoRepo
            .Setup(x => x.GetKudosByReceiverIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqKudos.Where(z => z.ReceiverId == y).ToList().AsEnumerable()));

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqUsers.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<GetKudosByReceiverIdResponse.Success>(result);
        if (result is GetKudosByReceiverIdResponse.Success success)
        {
            foreach(var kudo in success.ReceiverKudos)
            {
                Assert.Equal(request, kudo.ReceiverId);
            }
        }
    }

    [Theory]
    [InlineData("id")]
    [InlineData("id2")]
    public async void GetKudosByNonexistentReceiver_ReturnUserNotFound(string receiverId)
    {
        //Arrange
        string request = receiverId;

        var moqKudos = CreateMockKudoList();
        var moqUsers = CreateMockUserList();

        _mockKudoRepo
            .Setup(x => x.GetKudosByReceiverIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqKudos.Where(z => z.ReceiverId == y).ToList().AsEnumerable()));

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqUsers.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<GetKudosByReceiverIdResponse.UserNotFound>(result);
        Assert.Equal("A User with the given ReceiverId was not found", result.Message);
    }
}
