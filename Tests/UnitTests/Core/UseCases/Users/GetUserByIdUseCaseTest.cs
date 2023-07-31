using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.UseCases.Users.GetUserById;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Core.UseCases.Users;

public class GetUserByIdUseCaseShould : MockDataSetup
{
    private readonly GetUserByIdUseCase _useCase;
    private readonly Mock<IUserRepository> _mockUserRepo;

    public GetUserByIdUseCaseShould()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _useCase = new GetUserByIdUseCase(_mockUserRepo.Object);
    }

    [Fact]
    public async void GetUserById_ReturnSuccess()
    {
        //Arrange
        var request = "19df82ba-a964-4dbf-8013-69c120e938de";
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<GetUserByIdResponse.Success>(result);
        if (result is GetUserByIdResponse.Success success)
        {
            Assert.Equal(request, success.User.Id);
        }
    }

    [Theory]
    [InlineData("ae0a4ffd-e67a-49d6-bf68-ac8b456cb7b0")]
    [InlineData("1ccb742f-d204-47c5-8f49-ca6762d9c65f")]
    public async void GetNonexistentUserById_ReturnUserNotFound(string userId)
    {
        //Arrange
        var request = userId;
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<GetUserByIdResponse.UserNotFound>(result);
        Assert.Equal("A User with the given UserId was not found", result.Message);
    }
}
