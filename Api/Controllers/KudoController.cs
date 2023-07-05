using EmployeeRecognition.Api.Models;
using EmployeeRecognition.Core.Entities;
using EmployeeRecognition.Core.Interfaces.UseCases.Kudos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRecognition.Api.Controllers;

[ApiController]
[Route("kudos")]
public class KudoController : ControllerBase
{
    private readonly IAddKudoUseCase _addKudoUseCase;
    public KudoController(IAddKudoUseCase addKudoUseCase) 
    { 
        _addKudoUseCase = addKudoUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] KudoDto kudo)
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
}
