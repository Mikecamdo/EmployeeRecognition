using Laudatio.Api.Controllers;
using Laudatio.Core.Interfaces.UseCases.Kudos;
using Moq;

namespace Laudatio.Tests.UnitTests.Api;

public abstract class KudoControllerSetup : MockDataSetup
{
    protected readonly Mock<IGetAllKudosUseCase> _getAllKudosUseCase;
    protected readonly Mock<IGetKudosBySenderIdUseCase> _getKudosBySenderIdUseCase;
    protected readonly Mock<IGetKudosByReceiverIdUseCase> _getKudosByReceiverIdUseCase;
    protected readonly Mock<IAddKudoUseCase> _addKudoUseCase;
    protected readonly Mock<IDeleteKudoUseCase> _deleteKudoUseCase;

    public KudoControllerSetup()
    {
        _getAllKudosUseCase = new Mock<IGetAllKudosUseCase>();
        _getKudosBySenderIdUseCase = new Mock<IGetKudosBySenderIdUseCase>();
        _getKudosByReceiverIdUseCase = new Mock<IGetKudosByReceiverIdUseCase>();
        _addKudoUseCase = new Mock<IAddKudoUseCase>();
        _deleteKudoUseCase = new Mock<IDeleteKudoUseCase>();
    }

    public KudoController CreateKudoController()
    {
        return new KudoController(
            _getAllKudosUseCase.Object,
            _getKudosBySenderIdUseCase.Object,
            _getKudosByReceiverIdUseCase.Object,
            _addKudoUseCase.Object,
            _deleteKudoUseCase.Object
        );
    }
}
