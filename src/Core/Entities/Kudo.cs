using System.ComponentModel.DataAnnotations.Schema;

namespace Laudatio.Core.Entities;


[Table("kudos")]
public class Kudo
{
    public int Id { get; set; }
    public required string SenderId { get; set; }
    public required string ReceiverId { get; set; }
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

    public User? Sender { get; set; }
    public User? Receiver { get; set; }
    public IEnumerable<Comment>? Comments { get; set; }
}
