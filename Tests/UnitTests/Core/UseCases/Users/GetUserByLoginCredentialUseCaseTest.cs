using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.UseCases.Users.GetUserByLoginCredential;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Core.UseCases.Users;

public class GetUserByLoginCredentialUseCaseShould : MockDataSetup
{
    private readonly GetUserByLoginCredentialUseCase _useCase;
    private readonly Mock<IUserRepository> _mockUserRepo;

    public GetUserByLoginCredentialUseCaseShould()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _useCase = new GetUserByLoginCredentialUseCase(_mockUserRepo.Object);
    }

    [Fact]
    public async void GetUserByLoginCredential_ReturnUser()
    {
        //Arrange
        var request = new LoginCredential()
        {
            Name = "Michael",
            Password = "a_real_password"
        };
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByLoginCredentialAsync(It.IsAny<LoginCredential>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Name == request.Name && y.Password == request.Password)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<User>(result);
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Password, result.Password);
    }

    [Theory]
    [InlineData("123", "123")]
    [InlineData("A-Name", "foasdjhaosijdasodj")]
    public async void GetUserByInvalidLoginCredential_ReturnNull(string name, string password)
    {
        //Arrange
        var request = new LoginCredential()
        {
            Name = name,
            Password = password
        };
        var moqUsers = CreateMockUserList();

        _mockUserRepo
            .Setup(x => x.GetUserByLoginCredentialAsync(It.IsAny<LoginCredential>()))
            .Returns(Task.FromResult(moqUsers.FirstOrDefault(y => y.Name == request.Name && y.Password == request.Password)));

        //Act
        var result = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.Null(result);
    }
}
