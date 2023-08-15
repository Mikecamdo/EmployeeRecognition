using Laudatio.Core.UseCases.Users.DeleteUser;

namespace Laudatio.Core.Interfaces.UseCases.Users;

public interface IDeleteUserUseCase
{
    Task<DeleteUserResponse> ExecuteAsync(string userId);
}
