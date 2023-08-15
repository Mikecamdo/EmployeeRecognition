using Laudatio.Api.Models;
using Laudatio.Core.UseCases.Users.UpdateUser;

namespace Laudatio.Core.Interfaces.UseCases.Users;

public interface IUpdateUserUseCase
{
    Task<UpdateUserResponse> ExecuteAsync(UserModel updatedUserInfo);
}
