using Laudatio.Api.Models;
using Laudatio.Core.Entities;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.UseCases.Users.UpdateUser;
using Moq;

namespace Laudatio.Tests.UnitTests.Core.UseCases.Users;

public class UpdateUserUseCaseShould : MockDataSetup
{
    private readonly UpdateUserUseCase _useCase;
    private readonly Mock<IUserRepository> _mockUserRepo;

    public UpdateUserUseCaseShould()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _useCase = new UpdateUserUseCase(_mockUserRepo.Object);
    }

    [Fact]
    public async void UpdateUser_ReturnSuccess()
    {
        //Arrange
        var request = new UserModel()
        {
            Id = "19df82ba-a964-4dbf-8013-69c120e938de",
            Name = "Test",
            Password = "password",
            AvatarUrl = "url",
            Bio = "A bio"
        };
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request.Id)));

        _mockUserRepo
            .Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Name == request.Name)));

        _mockUserRepo
            .Setup(x => x.UpdateUserAsync(It.IsAny<User>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request.Id)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<UpdateUserResponse.Success>(result);
        if (result is UpdateUserResponse.Success success)
        {
            Assert.Equal(request.Id, success.UpdatedUser.Id);
            Assert.Equal(request.Name, success.UpdatedUser.Name);
            Assert.Equal(request.AvatarUrl, success.UpdatedUser.AvatarUrl);
        }
    }

    [Theory]
    [InlineData("ae0a4ffd-e67a-49d6-bf68-ac8b456cb7b0")]
    [InlineData("2627fe75-99a9-42b4-b3a6-174244280b4d")]
    [InlineData("1ccb742f-d204-47c5-8f49-ca6762d9c65f")]
    public async void UpdateNonexistentUser_ReturnUserNotFound(string userId)
    {
        //Arrange
        var request = new UserModel()
        {
            Id = userId,
            Name = "Test",
            Password = "password",
            AvatarUrl = "url",
            Bio = "A bio"
        };
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request.Id)));

        _mockUserRepo
            .Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Name == request.Name)));

        _mockUserRepo
            .Setup(x => x.UpdateUserAsync(It.IsAny<User>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request.Id)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<UpdateUserResponse.UserNotFound>(result);
        Assert.Equal("A User with the given UserId was not found", result.Message);
    }

    [Theory]
    [InlineData("test")]
    [InlineData("Bob")]
    [InlineData("Michael")]
    public async void UpdateUserWithExistingName_ReturnInvalidRequest(string newName)
    {
        //Arrange
        var request = new UserModel()
        {
            Id = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3",
            Name = newName,
            Password = "password",
            AvatarUrl = "url"
        };
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request.Id)));

        _mockUserRepo
            .Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Name == request.Name)));

        _mockUserRepo
            .Setup(x => x.UpdateUserAsync(It.IsAny<User>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request.Id)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<UpdateUserResponse.InvalidRequest>(result);
        Assert.Equal("Name already in use", result.Message);
    }
}
