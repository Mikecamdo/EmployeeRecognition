using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using EmployeeRecognition.Core.Interfaces.Repositories;
using EmployeeRecognition.Database.Context;
using EmployeeRecognition.Core.Interfaces.UseCases.Users;
using EmployeeRecognition.Core.UseCases.Users;
using EmployeeRecognition.Core.Repositories;
using EmployeeRecognition.Core.Interfaces.UseCases.Kudos;
using EmployeeRecognition.Core.UseCases.Kudos;
using EmployeeRecognition.Core.Interfaces.UseCases.Comments;
using EmployeeRecognition.Core.UseCases.Comments;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

DotNetEnv.Env.Load();

// Configure the MySQL database connection
var connectionString = $"Server={Env.GetString("MYSQLHOST")};Port={Env.GetString("MYSQLPORT")};Database={Env.GetString("MYSQLDATABASE")};Uid={Env.GetString("MYSQLUSER")};Pwd={Env.GetString("MYSQLPASSWORD")};Charset=utf8;";
builder.Services.AddDbContext<MySqlDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//Use cases
builder.Services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
builder.Services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
builder.Services.AddScoped<IAddUserUseCase, AddUserUseCase>();
builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();

builder.Services.AddScoped<IGetAllKudosUseCase, GetAllKudosUseCase>(); //FIXME can move all of these to their own folders to make this cleaner
builder.Services.AddScoped<IGetKudosBySenderIdUseCase, GetKudosBySenderIdUseCase>();
builder.Services.AddScoped<IGetKudosByReceiverIdUseCase, GetKudosByReceiverIdUseCase>();
builder.Services.AddScoped<IAddKudoUseCase, AddKudoUseCase>();
builder.Services.AddScoped<IDeleteKudoUseCase, DeleteKudoUseCase>();

builder.Services.AddScoped<IGetCommentsUseCase, GetCommentsUseCase>();
builder.Services.AddScoped<IAddCommentUseCase, AddCommentUseCase>();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IKudoRepository, KudoRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "employee-recognition", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "employee-recognition v1");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
