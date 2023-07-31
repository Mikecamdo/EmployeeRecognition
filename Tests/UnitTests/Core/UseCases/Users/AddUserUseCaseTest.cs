using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.UseCases.Users.AddUser;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Core.UseCases.Users;

public class AddUserUseCaseShould : MockDataSetup
{
    private readonly AddUserUseCase _useCase;
    private readonly Mock<IUserRepository> _mockUserRepo;

    public AddUserUseCaseShould()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _useCase = new AddUserUseCase(_mockUserRepo.Object);
    }

    [Fact]
    public async void AddUser_ReturnSuccess()
    {
        //Arrange
        var request = new User()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Test",
            Password = "password",
            AvatarUrl = "url"
        };
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Name == request.Name)));

        _mockUserRepo
            .Setup(x => x.AddUserAsync(It.IsAny<User>()))
            .Returns((User y) => Task.FromResult(y));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<AddUserResponse.Success>(result);
        if (result is AddUserResponse.Success success)
        {
            Assert.Equal(request.Id, success.NewUser.Id);
            Assert.Equal(request.Name, success.NewUser.Name);
            Assert.Equal(request.Password, success.NewUser.Password);
            Assert.Equal(request.AvatarUrl, success.NewUser.AvatarUrl);
        }
    }

    [Theory]
    [InlineData("test")]
    [InlineData("Bob")]
    [InlineData("Michael")]
    public async void AddUserWithExistingName_ReturnInvalidRequest(string name)
    {
        //Arrange
        var request = new User()
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Password = "password",
            AvatarUrl = "url"
        };
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Name == request.Name)));

        _mockUserRepo
            .Setup(x => x.AddUserAsync(It.IsAny<User>()))
            .Returns((User y) => Task.FromResult(y));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<AddUserResponse.InvalidRequest>(result);
        Assert.Equal("A user with that name already exists", result.Message);
    }
}
