using EmployeeRecognition.Core.Interfaces.UseCases.Users;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.UseCases.Users;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;
    public GetAllUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<User>> ExecuteAsync()
    {
        return await _userRepository.GetUsersAsync();
    }
}
