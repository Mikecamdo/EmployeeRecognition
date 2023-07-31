using EmployeeRecognition.Api.Converters;
using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.UseCases.Kudos.AddKudo;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Api;

public class KudoControllerShould : KudoControllerSetup
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void AddKudo(bool validKudo)
    {
        //Arrange
        if (validKudo)
        {
            _addKudoUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<Kudo>()))
                .Returns((Kudo y) =>
                    Task.FromResult<AddKudoResponse>(new AddKudoResponse.Success(
                        KudoModelConverter.ToModel(CreateMockKudoList().First()))));
        } else
        {
            _addKudoUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<Kudo>()))
                .Returns((Kudo y) =>
                    Task.FromResult<AddKudoResponse>(new AddKudoResponse.InvalidRequest("Invalid request")));
        }

        KudoDto request = new()
        {
            SenderId = "id",
            ReceiverId = "id",
            Title = "title",
            Message = "message",
            TeamPlayer = true,
            OneOfAKind = true,
            Creative = true,
            HighEnergy = true,
            Awesome = true,
            Achiever = true,
            Sweetness = true
        };

        //Act
        var ctrl = CreateKudoController();
        var result = ctrl.AddKudo(request).Result;

        //Assert
        if (validKudo)
        {
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var returnValue = okResult.Value;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(KudoModel));
        } else
        {
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();

            var returnValue = badRequestResult.Value;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(string));
            Assert.Equal("Invalid request", returnValue);
        }
    }
}
