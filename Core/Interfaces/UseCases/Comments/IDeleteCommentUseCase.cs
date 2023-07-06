namespace EmployeeRecognition.Core.Interfaces.UseCases.Comments;

public interface IDeleteCommentUseCase
{
    Task ExecuteAsync(int commentId);
}
