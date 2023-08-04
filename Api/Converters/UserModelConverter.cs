using Entity = EmployeeRecognition.Core.Entities.User;
using Model = EmployeeRecognition.Api.Models.UserModel;

namespace EmployeeRecognition.Api.Converters;

public class UserModelConverter
{
    public static Entity ToEntity(Model model)
    {
        return new Entity()
        {
            Id = model.Id,
            Name = model.Name,
            Password = model.Password,
            AvatarUrl = model.AvatarUrl,
            Bio = model.Bio
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
            Name = entity.Name,
            Password = entity.Password,
            AvatarUrl = entity.AvatarUrl,
            Bio = entity.Bio
        };
    }

    public static IEnumerable<Model> ToModel(IEnumerable<Entity> entities)
    {
        return entities.Select(e => ToModel(e));
    }
}
