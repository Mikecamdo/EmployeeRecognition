using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeRecognition.Database;

[Table("users")]
public class User
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Department { get; set; }
    public required string Team { get; set; }
    public required string AvatarUrl { get; set; }

}
