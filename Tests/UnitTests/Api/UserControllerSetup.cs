using EmployeeRecognition.Api.Controllers;
using EmployeeRecognition.Api.JwtFeatures;
using EmployeeRecognition.Core.Interfaces.UseCases.Users;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Api;

public abstract class UserControllerSetup : MockDataSetup
{
    private JwtHandler _jwtHandler;

    protected readonly Mock<IAddUserUseCase> _addUserUseCase;
    protected readonly Mock<IDeleteUserUseCase> _deleteUserUseCase;
    protected readonly Mock<IGetAllUsersUseCase> _getAllUsersUseCase;
    protected readonly Mock<IGetUserByIdUseCase> _getUserByIdUseCase;
    protected readonly Mock<IGetUserByLoginCredentialUseCase> _getUserByLoginCredentialUseCase;
    protected readonly Mock<IUpdateUserUseCase> _updateUserUseCase;

    public UserControllerSetup()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        _jwtHandler = new JwtHandler(configuration);
        _addUserUseCase = new Mock<IAddUserUseCase>();
        _deleteUserUseCase = new Mock<IDeleteUserUseCase>();
        _getAllUsersUseCase = new Mock<IGetAllUsersUseCase>();
        _getUserByIdUseCase = new Mock<IGetUserByIdUseCase>();
        _getUserByLoginCredentialUseCase = new Mock<IGetUserByLoginCredentialUseCase>();
        _updateUserUseCase = new Mock<IUpdateUserUseCase>();
    }

    public UserController CreateUserController()
    {
        return new UserController(
            _jwtHandler,
            _getAllUsersUseCase.Object,
            _getUserByIdUseCase.Object,
            _getUserByLoginCredentialUseCase.Object,
            _addUserUseCase.Object,
            _updateUserUseCase.Object,
            _deleteUserUseCase.Object
        );
    }
}
