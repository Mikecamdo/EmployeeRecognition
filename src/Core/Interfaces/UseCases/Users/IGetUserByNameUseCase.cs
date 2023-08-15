using Laudatio.Core.UseCases.Users.GetUserByName;

namespace Laudatio.Core.Interfaces.UseCases.Users;

public interface IGetUserByNameUseCase
{
    Task<GetUserByNameResponse> ExecuteAsync(string name);
}
