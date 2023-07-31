using EmployeeRecognition.Api.Converters;
using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.UseCases.Users.AddUser;
using EmployeeRecognition.Core.UseCases.Users.DeleteUser;
using EmployeeRecognition.Core.UseCases.Users.GetUserById;
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
        } else
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
        } else
        {
            result.Should().BeOfType<NotFoundObjectResult>();

            var okResult = result as NotFoundObjectResult;
            okResult.Should().NotBeNull();

            var returnValue = okResult.Value;
            returnValue.Should().NotBeNull();
            Assert.Equal("A User with the given UserId was not found", returnValue);
        }
    }
}
