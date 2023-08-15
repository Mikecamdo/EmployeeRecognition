using Laudatio.Api.Models;

namespace Laudatio.Core.Interfaces.UseCases.Users;

public interface IGetAllUsersUseCase
{
    Task<IEnumerable<UserModel>> ExecuteAsync();
}
