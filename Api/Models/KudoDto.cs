using Microsoft.VisualBasic;
using System.Reflection;

namespace EmployeeRecognition.Api.Models;

public class KudoDto
{
    public string Sender { get; set; }
    public string SenderId { get; set; }
    public string SenderAvatar { get; set; }
    public string Receiver { get; set; }
    public string ReceiverId { get; set; }
    public string ReceiverAvatar { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public Boolean TeamPlayer { get; set; }
    public Boolean OneOfAKind { get; set; }
    public Boolean Creative { get; set; }
    public Boolean HighEnergy { get; set; }
    public Boolean Awesome { get; set; }
    public Boolean Achiever { get; set; }
    public Boolean Sweetness { get; set; }
}
