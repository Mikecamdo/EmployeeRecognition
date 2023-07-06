using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Comments;

public interface IGetCommentsByKudoIdUseCase
{
    Task<IEnumerable<Comment>> ExecuteAsync(int kudoId);
}
