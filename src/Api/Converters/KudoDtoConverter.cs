using Dto = EmployeeRecognition.Api.Dtos.KudoDto;
using Model = EmployeeRecognition.Api.Models.KudoModel;

namespace EmployeeRecognition.Api.Converters;

public class KudoDtoConverter
{
    public static Model ToModel(Dto dto)
    {
        return new Model()
        {
            SenderId = dto.SenderId,
            ReceiverId = dto.ReceiverId,
            Title = dto.Title,
            Message = dto.Message,
            TeamPlayer = dto.TeamPlayer,
            OneOfAKind = dto.OneOfAKind,
            Creative = dto.Creative,
            HighEnergy = dto.HighEnergy,
            Awesome = dto.Awesome,
            Achiever = dto.Achiever,
            Sweetness = dto.Sweetness,
            TheDate = DateOnly.FromDateTime(DateTime.Now)
        };
    }

    public static IEnumerable<Model> ToModel(IEnumerable<Dto> dtos)
    {
        return dtos.Select(d => ToModel(d));
    }
}
