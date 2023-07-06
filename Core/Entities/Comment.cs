﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeRecognition.Core.Entities;

[Table("comments")]
public class Comment
{
    public int Id { get; set; }
    public required int KudoId { get; set; }
    public required string SenderId { get; set; }
    public required string SenderName { get; set; }
    public required string SenderAvatar { get; set; }
    public required string Message { get; set; }

}
