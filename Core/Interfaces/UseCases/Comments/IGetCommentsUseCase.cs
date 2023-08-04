using EmployeeRecognition.Api.Models;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Comments;

public interface IGetCommentsUseCase
{
    Task<IEnumerable<CommentModel>> ExecuteAsync();
}
