using Laudatio.Api.Models;
using Laudatio.Core.Entities;

namespace Laudatio.Core.Interfaces.UseCases.Users;

public interface IGetUserByLoginCredentialUseCase
{
    Task<User?> ExecuteAsync(LoginCredential loginCredential);
}
