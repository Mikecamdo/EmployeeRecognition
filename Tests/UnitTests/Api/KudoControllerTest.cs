using EmployeeRecognition.Api.Converters;
using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.UseCases.Kudos.AddKudo;
using EmployeeRecognition.Core.UseCases.Kudos.DeleteKudo;
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

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void DeleteKudo(bool kudoExists)
    {
        //Arrange
        if (kudoExists)
        {
            _deleteKudoUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<DeleteKudoResponse>(new DeleteKudoResponse.Success()));
        } else
        {
            _deleteKudoUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<DeleteKudoResponse>(new DeleteKudoResponse.KudoNotFound()));
        }
        int request = 1;

        //Act
        var ctrl = CreateKudoController();
        var result = ctrl.DeleteKudo(request).Result;

        //Assert
        if (kudoExists)
        {
            result.Should().BeOfType<NoContentResult>();

            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
        } else
        {
            result.Should().BeOfType<NotFoundObjectResult>();

            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();

            var returnValue = notFoundResult.Value as string;
            returnValue.Should().NotBeNull();
            returnValue.Should().BeOfType(typeof(string));
            Assert.Equal("A Kudo with the given KudoId was not found", returnValue);
        }
    }
}
