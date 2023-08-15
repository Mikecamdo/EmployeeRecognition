using Laudatio.Api.Models;
using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.UseCases.Users.GetAllUsers;
using Moq;

namespace Laudatio.Tests.UnitTests.Core.UseCases.Users;

public class GetAllUsersUseCaseShould : MockDataSetup
{
    private readonly GetAllUsersUseCase _useCase;
    private readonly Mock<IUserRepository> _mockUserRepo;

    public GetAllUsersUseCaseShould()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _useCase = new GetAllUsersUseCase(_mockUserRepo.Object);
    }

    [Fact]
    public async void GetAllUsers_ReturnAllUsers()
    {
        //Arrange
        var moqUsers = CreateMockUserList();
        _mockUserRepo
            .Setup(x => x.GetUsersAsync())
            .Returns(Task.FromResult(moqUsers));
        
        //Act
        IEnumerable<UserModel> result = await _useCase.ExecuteAsync();

        //Assert
        foreach(var user in result)
        {
            Assert.IsType<UserModel>(user);
        }
    }
}
