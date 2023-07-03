using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeRecognition.Database;

[Table("comments")]
public class Comment
{
    public required int Id { get; set; }
    public required int KudosId { get; set; }
    public required string SenderId { get; set; }
    public required string SenderName { get; set; }
    public required string SenderAvatar { get; set; }
    public required string Message { get; set; }

}
