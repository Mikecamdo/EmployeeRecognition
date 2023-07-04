using EmployeeRecognition.Core.Interfaces;
using EmployeeRecognition.Database;
using EmployeeRecognition.Interfaces;

namespace EmployeeRecognition.Core.UseCases;

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
