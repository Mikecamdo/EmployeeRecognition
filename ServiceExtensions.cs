using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Comments;
using EmployeeRecognition.Core.Interfaces.UseCases.Kudos;
using EmployeeRecognition.Core.Interfaces.UseCases.Users;
using EmployeeRecognition.Core.Repositories;
using EmployeeRecognition.Core.UseCases.Comments.AddComment;
using EmployeeRecognition.Core.UseCases.Comments.DeleteComment;
using EmployeeRecognition.Core.UseCases.Comments.GetComments;
using EmployeeRecognition.Core.UseCases.Comments.GetCommentsByKudoId;
using EmployeeRecognition.Core.UseCases.Comments.UpdateComment;
using EmployeeRecognition.Core.UseCases.Kudos.AddKudo;
using EmployeeRecognition.Core.UseCases.Kudos.DeleteKudo;
using EmployeeRecognition.Core.UseCases.Kudos.GetAllKudos;
using EmployeeRecognition.Core.UseCases.Kudos.GetKudosByReceiverId;
using EmployeeRecognition.Core.UseCases.Kudos.GetKudosBySenderId;
using EmployeeRecognition.Core.UseCases.Users.AddUser;
using EmployeeRecognition.Core.UseCases.Users.DeleteUser;
using EmployeeRecognition.Core.UseCases.Users.GetAllUsers;
using EmployeeRecognition.Core.UseCases.Users.GetUserById;
using EmployeeRecognition.Core.UseCases.Users.GetUserByLoginCredential;
using EmployeeRecognition.Core.UseCases.Users.GetUserByName;
using EmployeeRecognition.Core.UseCases.Users.UpdateUser;

namespace EmployeeRecognition;

public static class ServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IKudoRepository, KudoRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();

        return services;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
        services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
        services.AddScoped<IGetUserByNameUseCase, GetUserByNameUseCase>();
        services.AddScoped<IGetUserByLoginCredentialUseCase, GetUserByLoginCredentialUseCase>();
        services.AddScoped<IAddUserUseCase, AddUserUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();

        services.AddScoped<IGetAllKudosUseCase, GetAllKudosUseCase>();
        services.AddScoped<IGetKudosBySenderIdUseCase, GetKudosBySenderIdUseCase>();
        services.AddScoped<IGetKudosByReceiverIdUseCase, GetKudosByReceiverIdUseCase>();
        services.AddScoped<IAddKudoUseCase, AddKudoUseCase>();
        services.AddScoped<IDeleteKudoUseCase, DeleteKudoUseCase>();

        services.AddScoped<IGetCommentsUseCase, GetCommentsUseCase>();
        services.AddScoped<IGetCommentsByKudoIdUseCase, GetCommentsByKudoIdUseCase>();
        services.AddScoped<IAddCommentUseCase, AddCommentUseCase>();
        services.AddScoped<IUpdateCommentUseCase, UpdateCommentUseCase>();
        services.AddScoped<IDeleteCommentUseCase, DeleteCommentUseCase>();

        return services;
    }
}
