using EmployeeRecognition.Api.Dtos;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.UseCases.Users.UpdateUser;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Core.UseCases.Users;

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
        var request1 = "19df82ba-a964-4dbf-8013-69c120e938de";
        var request2 = new UserDto()
        {
            Name = "Test",
            Password = "password",
            AvatarUrl = "url"
        };
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request1)));

        _mockUserRepo
            .Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Name == request2.Name)));

        _mockUserRepo
            .Setup(x => x.UpdateUserAsync(It.IsAny<User>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request1)));

        //Act
        var result = await _useCase.ExecuteAsync(request1, request2);

        //Assert
        Assert.IsType<UpdateUserResponse.Success>(result);
        if (result is UpdateUserResponse.Success success)
        {
            Assert.Equal(request1, success.UpdatedUser.Id);
            Assert.Equal(request2.Name, success.UpdatedUser.Name);
            Assert.Equal(request2.Password, success.UpdatedUser.Password);
            Assert.Equal(request2.AvatarUrl, success.UpdatedUser.AvatarUrl);
        }
    }

    [Theory]
    [InlineData("ae0a4ffd-e67a-49d6-bf68-ac8b456cb7b0")]
    [InlineData("2627fe75-99a9-42b4-b3a6-174244280b4d")]
    [InlineData("1ccb742f-d204-47c5-8f49-ca6762d9c65f")]
    public async void UpdateNonexistentUser_ReturnUserNotFound(string userId)
    {
        //Arrange
        var request1 = userId;
        var request2 = new UserDto()
        {
            Name = "Test",
            Password = "password",
            AvatarUrl = "url"
        };
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request1)));

        _mockUserRepo
            .Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Name == request2.Name)));

        _mockUserRepo
            .Setup(x => x.UpdateUserAsync(It.IsAny<User>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request1)));

        //Act
        var result = await _useCase.ExecuteAsync(request1, request2);

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
        var request1 = "e1a07078-fd94-4554-812a-383c0367de90";
        var request2 = new UserDto()
        {
            Name = newName,
            Password = "password",
            AvatarUrl = "url"
        };
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request1)));

        _mockUserRepo
            .Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Name == request2.Name)));

        _mockUserRepo
            .Setup(x => x.UpdateUserAsync(It.IsAny<User>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request1)));

        //Act
        var result = await _useCase.ExecuteAsync(request1, request2);

        //Assert
        Assert.IsType<UpdateUserResponse.InvalidRequest>(result);
        Assert.Equal("Name already in use", result.Message);
    }
}
