using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.UseCases.Users.DeleteUser;
using Moq;

namespace Laudatio.Tests.UnitTests.Core.UseCases.Users;

public class DeleteUserUseCaseShould : MockDataSetup
{
    private readonly DeleteUserUseCase _useCase;
    private readonly Mock<IUserRepository> _mockUserRepo;

    public DeleteUserUseCaseShould()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _useCase = new DeleteUserUseCase(_mockUserRepo.Object);
    }

    [Fact]
    public async void DeleteUser_ReturnSuccess()
    {
        //Arrange
        var request = "e1a07078-fd94-4554-812a-383c0367de90";
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Id == request)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<DeleteUserResponse.Success>(result);
    }

    [Theory]
    [InlineData("edf55e75-5f6f-48b6-8a58-ed540cecbe58")]
    [InlineData("9b01940c-5c71-41e7-b348-15f2fc4e58c3")]
    public async void DeleteNonExistentUser_ReturnUserNotFound(string userId)
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
        Assert.IsType<DeleteUserResponse.UserNotFound>(result);
    }
}
