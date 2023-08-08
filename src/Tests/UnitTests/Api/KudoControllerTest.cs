using EmployeeRecognition.Api.Dtos;
using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Converters;
using EmployeeRecognition.Core.UseCases.Kudos.AddKudo;
using EmployeeRecognition.Core.UseCases.Kudos.DeleteKudo;
using EmployeeRecognition.Core.UseCases.Kudos.GetKudosByReceiverId;
using EmployeeRecognition.Core.UseCases.Kudos.GetKudosBySenderId;
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
                .Setup(x => x.ExecuteAsync(It.IsAny<KudoModel>()))
                .Returns((KudoModel y) =>
                    Task.FromResult<AddKudoResponse>(new AddKudoResponse.Success(
                        KudoModelConverter.ToModel(CreateMockKudoList().First()))));
        } else
        {
            _addKudoUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<KudoModel>()))
                .Returns((KudoModel y) =>
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

    [Fact]
    public void GetKudos()
    {
        //Arrange
        _getAllKudosUseCase
            .Setup(x => x.ExecuteAsync())
            .Returns(Task.FromResult(KudoModelConverter.ToModel(CreateMockKudoList())));

        //Act
        var ctrl = CreateKudoController();
        var result = ctrl.GetKudos().Result;

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
    public void GetKudosBySenderId(bool senderExists)
    {
        //Arrange
        if (senderExists)
        {
            _getKudosBySenderIdUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<string>()))
                .Returns((string y) => Task.FromResult<GetKudosBySenderIdResponse>(new GetKudosBySenderIdResponse.Success(
                    KudoModelConverter.ToModel(CreateMockKudoList().Where(z => z.SenderId == y).ToList()))));
        } else
        {
            _getKudosBySenderIdUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<string>()))
                .Returns((string y) => Task.FromResult<GetKudosBySenderIdResponse>(new GetKudosBySenderIdResponse.UserNotFound()));
        }
        string request = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3";

        //Act
        var ctrl = CreateKudoController();
        var result = ctrl.GetKudosBySenderId(request).Result;

        //Assert
        if (senderExists)
        {
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var returnValue = okResult.Value;
            returnValue.Should().NotBeNull();
        }
        else
        {
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();

            var returnValue = badRequestResult.Value;
            returnValue.Should().NotBeNull();
            Assert.Equal("A User with the given SenderId was not found", returnValue);
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GetKudosByReceiverId(bool receiverExists)
    {
        //Arrange
        if (receiverExists)
        {
            _getKudosByReceiverIdUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<string>()))
                .Returns((string y) => Task.FromResult<GetKudosByReceiverIdResponse>(new GetKudosByReceiverIdResponse.Success(
                    KudoModelConverter.ToModel(CreateMockKudoList().Where(z => z.ReceiverId == y).ToList()))));
        }
        else
        {
            _getKudosByReceiverIdUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<string>()))
                .Returns((string y) => Task.FromResult<GetKudosByReceiverIdResponse>(new GetKudosByReceiverIdResponse.UserNotFound()));
        }
        string request = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3";

        //Act
        var ctrl = CreateKudoController();
        var result = ctrl.GetKudosByReceiverId(request).Result;

        //Assert
        if (receiverExists)
        {
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var returnValue = okResult.Value;
            returnValue.Should().NotBeNull();
        }
        else
        {
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();

            var returnValue = badRequestResult.Value;
            returnValue.Should().NotBeNull();
            Assert.Equal("A User with the given ReceiverId was not found", returnValue);
        }
    }
}
