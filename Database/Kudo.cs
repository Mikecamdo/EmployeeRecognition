using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeRecognition.Database;


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
    public required Boolean TeamPlayer { get; set; }
    public required Boolean OneOfAKind { get; set; }
    public required Boolean Creative { get; set; }
    public required Boolean HighEnergy { get; set; }
    public required Boolean Awesome { get; set; }
    public required Boolean Achiever { get; set; }
    public required Boolean Sweetness { get; set; }
    public required DateOnly TheDate { get; set; }

}
