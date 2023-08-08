using EmployeeRecognition.Core.UseCases.Comments.GetCommentsByKudoId;

namespace EmployeeRecognition.Core.Interfaces.UseCases.Comments;

public interface IGetCommentsByKudoIdUseCase
{
    Task<GetCommentsByKudoIdResponse> ExecuteAsync(int kudoId);
}
