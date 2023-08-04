namespace EmployeeRecognition.Api.Models;

public class CommentModel
{
    public int Id { get; set; }
    public int KudoId { get; set; }
    public string SenderId { get; set; }
    public string SenderName { get; set; }
    public string SenderAvatar { get; set; }
    public string Message { get; set; }
}
