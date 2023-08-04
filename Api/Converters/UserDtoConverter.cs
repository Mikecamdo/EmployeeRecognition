using Dto = EmployeeRecognition.Api.Dtos.UserDto;
using Model = EmployeeRecognition.Api.Models.UserModel;

namespace EmployeeRecognition.Api.Converters;

public class UserDtoConverter
{
    public static Model ToModel(Dto dto)
    {
        return new Model()
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Password = dto.Password,
            AvatarUrl = dto.AvatarUrl,
            Bio = dto.Bio
        };
    }

    public static IEnumerable<Model> ToModel(IEnumerable<Dto> dtos)
    {
        return dtos.Select(d => ToModel(d));
    }
}
