﻿using EmployeeRecognition.Api.Converters;
using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.UseCases.Users.AddUser;
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
}
