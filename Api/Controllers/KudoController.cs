﻿using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.UseCases.Kudos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRecognition.Api.Controllers;

[ApiController]
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
    public async Task<IActionResult> GetKudos()
    {
        var allKudos = await _getAllKudosUseCase.ExecuteAsync();
        return Ok(allKudos);
    }

    [HttpGet("sender/{senderId}")]
    public async Task<IActionResult> GetKudosBySenderId([FromRoute] string senderId)
    {
        var senderKudos = await _getKudosBySenderIdUseCase.ExecuteAsync(senderId);
        return Ok(senderKudos);
    }

    [HttpGet("receiver/{receiverId}")]
    public async Task<IActionResult> GetKudosByReceiverId([FromRoute] string receiverId)
    {
        var receiverKudos = await _getKudosByReceiverIdUseCase.ExecuteAsync(receiverId);
        return Ok(receiverKudos);
    }

    [HttpPost]
    public async Task<IActionResult> AddKudo([FromBody] KudoDto kudo)
    {
        var newKudo = new Kudo() //FIXME need to move to a converter
        {
            Sender = kudo.Sender,
            SenderId = kudo.SenderId,
            SenderAvatar = kudo.SenderAvatar,

            Receiver = kudo.Receiver,
            ReceiverId = kudo.ReceiverId,
            ReceiverAvatar = kudo.ReceiverAvatar,

            Title = kudo.Title,
            Message = kudo.Message,
            TeamPlayer = kudo.TeamPlayer,
            OneOfAKind = kudo.OneOfAKind,
            Creative = kudo.Creative,
            HighEnergy = kudo.HighEnergy,
            Awesome = kudo.Awesome,
            Achiever = kudo.Achiever,
            Sweetness = kudo.Sweetness,
            TheDate = DateOnly.FromDateTime(DateTime.Now)
        };

        var addedKudo = await _addKudoUseCase.ExecuteAsync(newKudo);
        return Ok(addedKudo);
    }

    [HttpDelete("{kudoId}")]
    public async Task<IActionResult> DeleteKudo([FromRoute] int kudoId)
    {
        await _deleteKudoUseCase.ExecuteAsync(kudoId);
        return NoContent();
    }
}
