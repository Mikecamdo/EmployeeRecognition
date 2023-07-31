using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.UseCases.Kudos.AddKudo;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Core.UseCases.Kudos;

public class AddKudoUseCaseShould : MockDataSetup
{
    private readonly AddKudoUseCase _useCase;
    private readonly Mock<IKudoRepository> _mockKudoRepo;
    private readonly Mock<IUserRepository> _mockUserRepo;

    public AddKudoUseCaseShould()
    {
        _mockKudoRepo = new Mock<IKudoRepository>();
        _mockUserRepo = new Mock<IUserRepository>();
        _useCase = new AddKudoUseCase(_mockKudoRepo.Object, _mockUserRepo.Object);
    }

    [Fact]
    public async void AddKudo_ReturnSuccess()
    {
        //Arrange
        var request = new Kudo()
        {
            SenderId = "19df82ba-a964-4dbf-8013-69c120e938de",
            ReceiverId = "e1a07078-fd94-4554-812a-383c0367de90",
            Title = "title",
            Message = "message",
            TeamPlayer = true,
            OneOfAKind = false,
            Creative = false,
            HighEnergy = true,
            Awesome = true,
            Achiever = true,
            Sweetness = true,
            TheDate = DateOnly.FromDateTime(DateTime.Now)
        };
        var moqUsers = CreateMockUserList();

        _mockKudoRepo
            .Setup(x => x.AddKudoAsync(It.IsAny<Kudo>()))
            .Returns((Kudo y) => Task.FromResult(y));

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqUsers.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<AddKudoResponse.Success>(result);
        if (result is AddKudoResponse.Success success)
        {
            Assert.Equal(request.SenderId, success.NewKudo.SenderId);
            Assert.Equal(request.ReceiverId, success.NewKudo.ReceiverId);
            Assert.Equal(request.Title, success.NewKudo.Title);
            Assert.Equal(request.Message, success.NewKudo.Message);
        }
    }

    [Theory]
    [InlineData("id")]
    [InlineData("id2")]
    public async void AddKudoWithNonexistentSender_ReturnInvalidRequest(string senderId)
    {
        //Arrange
        var request = new Kudo()
        {
            SenderId = senderId,
            ReceiverId = "e1a07078-fd94-4554-812a-383c0367de90",
            Title = "title",
            Message = "message",
            TeamPlayer = true,
            OneOfAKind = false,
            Creative = false,
            HighEnergy = true,
            Awesome = true,
            Achiever = true,
            Sweetness = true,
            TheDate = DateOnly.FromDateTime(DateTime.Now)
        };
        var moqUsers = CreateMockUserList();

        _mockKudoRepo
            .Setup(x => x.AddKudoAsync(It.IsAny<Kudo>()))
            .Returns((Kudo y) => Task.FromResult(y));

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqUsers.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<AddKudoResponse.InvalidRequest>(result);
        Assert.Equal("A User with the given SenderId was not found", result.Message);
    }

    [Theory]
    [InlineData("id")]
    [InlineData("id2")]
    public async void AddKudoWithNonexistentReceiver_ReturnInvalidRequest(string receiverId)
    {
        //Arrange
        var request = new Kudo()
        {
            SenderId = "19df82ba-a964-4dbf-8013-69c120e938de",
            ReceiverId = receiverId,
            Title = "title",
            Message = "message",
            TeamPlayer = true,
            OneOfAKind = false,
            Creative = false,
            HighEnergy = true,
            Awesome = true,
            Achiever = true,
            Sweetness = true,
            TheDate = DateOnly.FromDateTime(DateTime.Now)
        };
        var moqUsers = CreateMockUserList();

        _mockKudoRepo
            .Setup(x => x.AddKudoAsync(It.IsAny<Kudo>()))
            .Returns((Kudo y) => Task.FromResult(y));

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqUsers.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<AddKudoResponse.InvalidRequest>(result);
        Assert.Equal("A User with the given ReceiverId was not found", result.Message);
    }

    [Theory]
    [InlineData("19df82ba-a964-4dbf-8013-69c120e938de")]
    [InlineData("e1a07078-fd94-4554-812a-383c0367de90")]
    public async void AddKudoWithSameSenderAndReceiver_ReturnInvalidRequest(string userId)
    {
        //Arrange
        var request = new Kudo()
        {
            SenderId = userId,
            ReceiverId = userId,
            Title = "title",
            Message = "message",
            TeamPlayer = true,
            OneOfAKind = false,
            Creative = false,
            HighEnergy = true,
            Awesome = true,
            Achiever = true,
            Sweetness = true,
            TheDate = DateOnly.FromDateTime(DateTime.Now)
        };
        var moqUsers = CreateMockUserList();

        _mockKudoRepo
            .Setup(x => x.AddKudoAsync(It.IsAny<Kudo>()))
            .Returns((Kudo y) => Task.FromResult(y));

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns((string y) => Task.FromResult(moqUsers.FirstOrDefault(z => z.Id == y)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<AddKudoResponse.InvalidRequest>(result);
        Assert.Equal("Cannot send kudo to yourself", result.Message);
    }
}
