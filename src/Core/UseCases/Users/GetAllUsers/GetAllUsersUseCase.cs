using EmployeeRecognition.Core.Interfaces.UseCases.Users;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Converters;
using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.UseCases.Users.GetAllUsers;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;
    public GetAllUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<UserModel>> ExecuteAsync()
    {
        return UserModelConverter.ToModel(await _userRepository.GetUsersAsync());
    }
}
