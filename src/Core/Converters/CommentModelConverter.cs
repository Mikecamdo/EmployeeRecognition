using Entity = Laudatio.Core.Entities.Comment;
using Model = Laudatio.Api.Models.CommentModel;

namespace Laudatio.Core.Converters;

public class CommentModelConverter
{
    public static Entity ToEntity(Model model)
    {
        return new Entity()
        {
            Id = model.Id,
            KudoId = model.KudoId,
            SenderId = model.SenderId,
            Message = model.Message
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
            KudoId = entity.KudoId,
            SenderId = entity.SenderId,
            SenderName = entity.Sender?.Name,
            SenderAvatar = entity.Sender?.AvatarUrl,
            Message = entity.Message
        };
    }

    public static IEnumerable<Model> ToModel(IEnumerable<Entity> entities)
    {
        return entities.Select(e => ToModel(e));
    }
}
