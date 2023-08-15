using Dto = Laudatio.Api.Dtos.CommentDto;
using Model = Laudatio.Api.Models.CommentModel;

namespace Laudatio.Api.Converters;

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
