using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeRecognition.Core.Entities;


[Table("kudos")]
public class Kudo
{
    public int Id { get; set; }
    public required string Sender { get; set; }
    public required string SenderId { get; set; }
    public required string SenderAvatar { get; set; }

    public required string Receiver { get; set; }
    public required string ReceiverId { get; set; }
    public required string ReceiverAvatar { get; set; }

    public required string Title { get; set; }
    public required string Message { get; set; }
    public required bool TeamPlayer { get; set; }
    public required bool OneOfAKind { get; set; }
    public required bool Creative { get; set; }
    public required bool HighEnergy { get; set; }
    public required bool Awesome { get; set; }
    public required bool Achiever { get; set; }
    public required bool Sweetness { get; set; }
    public required DateOnly TheDate { get; set; }

}
