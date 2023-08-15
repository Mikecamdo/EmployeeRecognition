using Laudatio.Core.Interfaces.Repositories;
using Laudatio.Core.Interfaces.UseCases.Comments;
using Laudatio.Core.Interfaces.UseCases.Kudos;
using Laudatio.Core.Interfaces.UseCases.Users;
using Laudatio.Core.Repositories;
using Laudatio.Core.UseCases.Comments.AddComment;
using Laudatio.Core.UseCases.Comments.DeleteComment;
using Laudatio.Core.UseCases.Comments.GetComments;
using Laudatio.Core.UseCases.Comments.GetCommentsByKudoId;
using Laudatio.Core.UseCases.Comments.UpdateComment;
using Laudatio.Core.UseCases.Kudos.AddKudo;
using Laudatio.Core.UseCases.Kudos.DeleteKudo;
using Laudatio.Core.UseCases.Kudos.GetAllKudos;
using Laudatio.Core.UseCases.Kudos.GetKudosByReceiverId;
using Laudatio.Core.UseCases.Kudos.GetKudosBySenderId;
using Laudatio.Core.UseCases.Users.AddUser;
using Laudatio.Core.UseCases.Users.DeleteUser;
using Laudatio.Core.UseCases.Users.GetAllUsers;
using Laudatio.Core.UseCases.Users.GetUserById;
using Laudatio.Core.UseCases.Users.GetUserByLoginCredential;
using Laudatio.Core.UseCases.Users.GetUserByName;
using Laudatio.Core.UseCases.Users.UpdateUser;

namespace Laudatio;

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
