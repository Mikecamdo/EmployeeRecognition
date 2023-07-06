using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Comments;

public interface IGetCommentsUseCase
{
    Task<IEnumerable<Comment>> ExecuteAsync();
}
