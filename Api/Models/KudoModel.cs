namespace EmployeeRecognition.Api.Models;

public class KudoModel
{
    public required int Id { get; set; }
    public required string? SenderName { get; set; }
    public required string SenderId { get; set; }
    public required string? SenderAvatarUrl { get; set; }
    public required string? ReceiverName { get; set; }
    public required string ReceiverId { get; set; }
    public required string? ReceiverAvatarUrl { get; set; }
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
