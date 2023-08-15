using Laudatio.Api.Models;
using Laudatio.Core.UseCases.Users.AddUser;

namespace Laudatio.Core.Interfaces.UseCases.Users;

public interface IAddUserUseCase
{
    Task<AddUserResponse> ExecuteAsync(UserModel user);
}
