using Laudatio.Api.Converters;
using Laudatio.Api.Dtos;
using Laudatio.Api.Models;
using Laudatio.Core.Interfaces.UseCases.Kudos;
using Laudatio.Core.UseCases.Kudos.AddKudo;
using Laudatio.Core.UseCases.Kudos.DeleteKudo;
using Laudatio.Core.UseCases.Kudos.GetKudosByReceiverId;
using Laudatio.Core.UseCases.Kudos.GetKudosBySenderId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laudatio.Api.Controllers;

[Authorize]
[Route("kudos")]
public class KudoController : ControllerBase
{
    private readonly IGetAllKudosUseCase _getAllKudosUseCase;
    private readonly IGetKudosBySenderIdUseCase _getKudosBySenderIdUseCase;
    private readonly IGetKudosByReceiverIdUseCase _getKudosByReceiverIdUseCase;
    private readonly IAddKudoUseCase _addKudoUseCase;
    private readonly IDeleteKudoUseCase _deleteKudoUseCase;

    public KudoController(
        IGetAllKudosUseCase getAllKudosUseCase,
        IGetKudosBySenderIdUseCase getKudosBySenderIdUseCase,
        IGetKudosByReceiverIdUseCase getKudosByReceiverIdUseCase,
        IAddKudoUseCase addKudoUseCase,
        IDeleteKudoUseCase deleteKudoUseCase)
    {
        _getAllKudosUseCase = getAllKudosUseCase;
        _getKudosBySenderIdUseCase = getKudosBySenderIdUseCase;
        _getKudosByReceiverIdUseCase = getKudosByReceiverIdUseCase;
        _addKudoUseCase = addKudoUseCase;
        _deleteKudoUseCase = deleteKudoUseCase;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<KudoModel>))]
    public async Task<IActionResult> GetKudos()
    {
        var allKudos = await _getAllKudosUseCase.ExecuteAsync();
        return Ok(allKudos);
    }

    [HttpGet("sender/{senderId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<KudoModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetKudosBySenderId([FromRoute] string senderId)
    {
        var getKudosBySenderIdResponse = await _getKudosBySenderIdUseCase.ExecuteAsync(senderId);

        return getKudosBySenderIdResponse switch
        {
            GetKudosBySenderIdResponse.Success success => Ok(success.SenderKudos),
            GetKudosBySenderIdResponse.UserNotFound => BadRequest(getKudosBySenderIdResponse.Message),
            _ => Problem("Unexpected response")
        };
    }

    [HttpGet("receiver/{receiverId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<KudoModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetKudosByReceiverId([FromRoute] string receiverId)
    {
        var getKudosByReceiverIdResponse = await _getKudosByReceiverIdUseCase.ExecuteAsync(receiverId);

        return getKudosByReceiverIdResponse switch
        {
            GetKudosByReceiverIdResponse.Success success => Ok(success.ReceiverKudos),
            GetKudosByReceiverIdResponse.UserNotFound => BadRequest(getKudosByReceiverIdResponse.Message),
            _ => Problem("Unexpected response")
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KudoModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddKudo([FromBody] KudoDto kudo)
    {
        if (kudo == null)
        {
            return BadRequest("Missing/invalid parameters");
        }

        var newKudo = KudoDtoConverter.ToModel(kudo);

        var addKudoResponse = await _addKudoUseCase.ExecuteAsync(newKudo);

        return addKudoResponse switch
        {
            AddKudoResponse.Success success => Ok(success.NewKudo),
            AddKudoResponse.InvalidRequest => BadRequest(addKudoResponse.Message),
            _ => Problem("Unexpected response")
        };
    }

    [HttpDelete("{kudoId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteKudo([FromRoute] int kudoId)
    {
        var deleteKudoResponse = await _deleteKudoUseCase.ExecuteAsync(kudoId);

        return deleteKudoResponse switch
        {
            DeleteKudoResponse.Success => NoContent(),
            DeleteKudoResponse.KudoNotFound => NotFound(deleteKudoResponse.Message),
            _ => Problem("Unexpected response")
        };
    }
}
