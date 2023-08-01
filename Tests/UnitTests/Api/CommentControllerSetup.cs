using EmployeeRecognition.Api.Controllers;
using EmployeeRecognition.Core.Interfaces.UseCases.Comments;
using Moq;

namespace EmployeeRecognition.Tests.UnitTests.Api;

public abstract class CommentControllerSetup : MockDataSetup
{
    protected readonly Mock<IGetCommentsUseCase> _getCommentsUseCase;
    protected readonly Mock<IGetCommentsByKudoIdUseCase> _getCommentsByKudoIdUseCase;
    protected readonly Mock<IAddCommentUseCase> _addCommentUseCase;
    protected readonly Mock<IUpdateCommentUseCase> _updateCommentUseCase;
    protected readonly Mock<IDeleteCommentUseCase> _deleteCommentUseCase;

    public CommentControllerSetup()
    {
        _getCommentsUseCase = new Mock<IGetCommentsUseCase>();
        _getCommentsByKudoIdUseCase = new Mock<IGetCommentsByKudoIdUseCase>();
        _addCommentUseCase = new Mock<IAddCommentUseCase>();
        _updateCommentUseCase = new Mock<IUpdateCommentUseCase>();
        _deleteCommentUseCase = new Mock<IDeleteCommentUseCase>();
    }

    public CommentController CreateCommentController()
    {
        return new CommentController(
            _getCommentsUseCase.Object,
            _getCommentsByKudoIdUseCase.Object,
            _addCommentUseCase.Object,
            _updateCommentUseCase.Object,
            _deleteCommentUseCase.Object
        );
    }
}
