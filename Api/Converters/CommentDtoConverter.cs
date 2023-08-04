using Dto = EmployeeRecognition.Api.Dtos.CommentDto;
using Model = EmployeeRecognition.Api.Models.CommentModel;

namespace EmployeeRecognition.Api.Converters;

public class CommentDtoConverter
{
    public static Model ToModel(Dto dto)
    {
        return new Model()
        {
            KudoId = dto.KudoId,
            SenderId = dto.SenderId,
            Message = dto.Message
        };
    }

    public static IEnumerable<Model> ToModel(IEnumerable<Dto> dtos)
    {
        return dtos.Select(d => ToModel(d));
    }
}
