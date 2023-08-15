namespace Laudatio.Api.Models;

public class KudoModel
{
    public int Id { get; set; }
    public string? SenderName { get; set; }
    public string SenderId { get; set; }
    public string? SenderAvatarUrl { get; set; }
    public string? ReceiverName { get; set; }
    public string ReceiverId { get; set; }
    public string? ReceiverAvatarUrl { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public bool TeamPlayer { get; set; }
    public bool OneOfAKind { get; set; }
    public bool Creative { get; set; }
    public bool HighEnergy { get; set; }
    public bool Awesome { get; set; }
    public bool Achiever { get; set; }
    public bool Sweetness { get; set; }
    public DateOnly TheDate { get; set; }
}
