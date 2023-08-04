using EmployeeRecognition.Api.Dto;
using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Converters;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.UseCases.Users.AddUser;
using EmployeeRecognition.Core.UseCases.Users.DeleteUser;
using EmployeeRecognition.Core.UseCases.Users.GetUserById;
using EmployeeRecognition.Core.UseCases.Users.UpdateUser;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Api;

public class UserControllerShould : UserControllerSetup
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void AddUser(bool userAlreadyExists)
    {
        //Arrange
        if (userAlreadyExists)
        {
            _addUserUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<User>()))
                .Returns((User y) =>
                    Task.FromResult<AddUserResponse>(new AddUserResponse.InvalidRequest(
                        "A user with that name already exists")));
        }
        else
        {
            _addUserUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<User>()))
                .Returns((User y) =>
                    Task.FromResult<AddUserResponse>(new AddUserResponse.Success(
                        UserModelConverter.ToModel(CreateMockUserList().First()))));
        }

        UserDto request = new()
        {
            Name = "name",
            Password = "password",
            AvatarUrl = ""
        };

        //Act
        var ctrl = CreateUserController();
        var result = ctrl.AddUser(request).Result;

        //Assert
        if (userAlreadyExists)
        {
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();

            var returnValue = badRequestResult.Value as SignupResponse;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(SignupResponse));
            Assert.Equal("A user with that name already exists", returnValue.ErrorMessage);
            Assert.False(returnValue.IsSignupSuccessful);
        }
        else
        {
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var returnValue = okResult.Value as SignupResponse;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(SignupResponse));
            Assert.True(returnValue.IsSignupSuccessful);
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void DeleteUser(bool userExists)
    {
        //Arrange
        if (userExists)
        {
            _deleteUserUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<DeleteUserResponse>(new DeleteUserResponse.Success()));
        }
        else
        {
            _deleteUserUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<DeleteUserResponse>(new DeleteUserResponse.UserNotFound()));
        }
        string request = "id";

        //Act
        var ctrl = CreateUserController();
        var result = ctrl.DeleteUser(request).Result;

        //Assert
        if (userExists)
        {
            result.Should().BeOfType<NoContentResult>();

            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
        }
        else
        {
            result.Should().BeOfType<NotFoundObjectResult>();

            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();

            var returnValue = notFoundResult.Value as string;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(string));
            Assert.Equal("A User with the given UserId was not found", returnValue);
        }
    }

    [Fact]
    public void GetUsers()
    {
        //Arrange
        _getAllUsersUseCase
            .Setup(x => x.ExecuteAsync())
            .Returns(Task.FromResult(CreateMockUserList()));

        //Act
        var ctrl = CreateUserController();
        var result = ctrl.GetUsers().Result;

        //Assert
        result.Should().BeOfType<OkObjectResult>();

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();

        var returnValue = okResult.Value;
        returnValue.Should().NotBeNull();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GetUserById(bool userExists)
    {
        //Arrange
        if (userExists)
        {
            _getUserByIdUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<string>()))
                .Returns((string y) => Task.FromResult<GetUserByIdResponse>(new GetUserByIdResponse.Success(
                    UserModelConverter.ToModel(CreateMockUserList().First(z => z.Id == y)))));
        }
        else
        {
            _getUserByIdUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<string>()))
                .Returns((string y) => Task.FromResult<GetUserByIdResponse>(new GetUserByIdResponse.UserNotFound()));
        }
        string request = "19df82ba-a964-4dbf-8013-69c120e938de";

        //Act
        var ctrl = CreateUserController();
        var result = ctrl.GetUserById(request).Result;

        //Assert
        if (userExists)
        {
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var returnValue = okResult.Value;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(UserModel));
        }
        else
        {
            result.Should().BeOfType<NotFoundObjectResult>();

            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();

            var returnValue = notFoundResult.Value;
            returnValue.Should().NotBeNull();
            Assert.Equal("A User with the given UserId was not found", returnValue);
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GetUserByLoginCredentials(bool validCredential)
    {
        //Arrange
        if (validCredential)
        {
            _getUserByLoginCredentialUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<LoginCredential>()))
                .Returns((LoginCredential y) => Task.FromResult((CreateMockUserList().First())));
        }
        else
        {
            _getUserByLoginCredentialUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<LoginCredential>()))
                .Returns((LoginCredential y) => Task.FromResult((CreateMockUserList().FirstOrDefault(z => z.Id == "id"))));
        }

        LoginCredential request = new()
        {
            Name = "Name",
            Password = "Password"
        };

        //Act
        var ctrl = CreateUserController();
        var result = ctrl.GetUserByLoginCredentials(request).Result;

        //Assert
        if (validCredential)
        {
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var returnValue = okResult.Value;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(LoginResponse));
        }
        else
        {
            result.Should().BeOfType<UnauthorizedObjectResult>();

            var unauthorizedResult = result as UnauthorizedObjectResult;
            unauthorizedResult.Should().NotBeNull();

            var returnValue = unauthorizedResult.Value as LoginResponse;
            returnValue.Should().NotBeNull();
            Assert.Equal("Invalid Login", returnValue.ErrorMessage);
        }
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public void UpdateUser(bool userExists, bool nameAlreadyTaken)
    {
        //Arrange
        if (userExists && nameAlreadyTaken)
        {
            _updateUserUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<UserDto>()))
                .Returns(Task.FromResult<UpdateUserResponse>(new UpdateUserResponse.InvalidRequest("Name already in use")));
        }
        else if (userExists && !nameAlreadyTaken)
        {
            _updateUserUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<UserDto>()))
                .Returns(Task.FromResult<UpdateUserResponse>(new UpdateUserResponse.Success(
                    UserModelConverter.ToModel(CreateMockUserList().First()))));
        }
        else
        {
            _updateUserUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<UserDto>()))
                .Returns(Task.FromResult<UpdateUserResponse>(new UpdateUserResponse.UserNotFound()));
        }

        string request1 = "id";
        UserDto request2 = new()
        {
            Name = "name",
            Password = "password",
            AvatarUrl = "url"
        };

        //Act
        var ctrl = CreateUserController();
        var result = ctrl.UpdateUser(request1, request2).Result;

        //Assert
        if (userExists && nameAlreadyTaken)
        {
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();

            var returnValue = badRequestResult.Value;
            returnValue.Should().NotBeNull();
            Assert.Equal("Name already in use", returnValue);
        }
        else if (userExists && !nameAlreadyTaken)
        {
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var returnValue = okResult.Value;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(SignupResponse));
        }
        else
        {
            result.Should().BeOfType<NotFoundObjectResult>();

            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();

            var returnValue = notFoundResult.Value;
            returnValue.Should().NotBeNull();
            Assert.Equal("A User with the given UserId was not found", returnValue);
        }
    }
}
