using Laudatio.Core.UseCases.Users.GetUserById;

namespace Laudatio.Core.Interfaces.UseCases.Users;

public interface IGetUserByIdUseCase
{
    Task<GetUserByIdResponse> ExecuteAsync(string userId);
}
