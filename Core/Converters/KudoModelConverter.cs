using Entity = EmployeeRecognition.Core.Entities.Kudo;
using Model = EmployeeRecognition.Api.Models.KudoModel;

namespace EmployeeRecognition.Core.Converters;

public class KudoModelConverter
{
    public static Entity ToEntity(Model model)
    {
        return new Entity()
        {
            Id = model.Id,
            SenderId = model.SenderId,
            ReceiverId = model.ReceiverId,
            Title = model.Title,
            Message = model.Message,
            TeamPlayer = model.TeamPlayer,
            OneOfAKind = model.OneOfAKind,
            Creative = model.Creative,
            HighEnergy = model.HighEnergy,
            Awesome = model.Awesome,
            Achiever = model.Achiever,
            Sweetness = model.Sweetness,
            TheDate = model.TheDate
        };
    }

    public static IEnumerable<Entity> ToEntity(IEnumerable<Model> models)
    {
        return models.Select(m => ToEntity(m));
    }

    public static Model ToModel(Entity entity)
    {
        return new Model()
        {
            Id = entity.Id,
            SenderName = entity.Sender?.Name,
            SenderId = entity.SenderId,
            SenderAvatarUrl = entity.Sender?.AvatarUrl,
            ReceiverName = entity.Receiver?.Name,
            ReceiverId = entity.ReceiverId,
            ReceiverAvatarUrl = entity.Receiver?.AvatarUrl,
            Title = entity.Title,
            Message = entity.Message,
            TeamPlayer = entity.TeamPlayer,
            OneOfAKind = entity.OneOfAKind,
            Creative = entity.Creative,
            HighEnergy = entity.HighEnergy,
            Awesome = entity.Awesome,
            Achiever = entity.Achiever,
            Sweetness = entity.Sweetness,
            TheDate = entity.TheDate
        };
    }

    public static IEnumerable<Model> ToModel(IEnumerable<Entity> entities)
    {
        return entities.Select(e => ToModel(e));
    }
}
