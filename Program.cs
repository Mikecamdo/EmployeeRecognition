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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EmployeeRecognition.Api.JwtFeatures;
using System.Reflection;
using EmployeeRecognition.Core.UseCases.Comments.AddComment;
using EmployeeRecognition.Core.UseCases.Comments.DeleteComment;
using EmployeeRecognition.Core.UseCases.Comments.GetCommentsByKudoId;
using EmployeeRecognition.Core.UseCases.Comments.GetComments;
using EmployeeRecognition.Core.UseCases.Comments.UpdateComment;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

DotNetEnv.Env.Load();

// Configure the MySQL database connection
var connectionString = $"Server={Env.GetString("MYSQLHOST")};Port={Env.GetString("MYSQLPORT")};Database={Env.GetString("MYSQLDATABASE")};Uid={Env.GetString("MYSQLUSER")};Pwd={Env.GetString("MYSQLPASSWORD")};Charset=utf8;";
builder.Services.AddDbContext<MySqlDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});

//JWT Authentication
builder.Services.AddScoped<JwtHandler>();

//Use cases
builder.Services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
builder.Services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
builder.Services.AddScoped<IGetUserByLoginCredentialUseCase, GetUserByLoginCredentialUseCase>();
builder.Services.AddScoped<IAddUserUseCase, AddUserUseCase>();
builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();

builder.Services.AddScoped<IGetAllKudosUseCase, GetAllKudosUseCase>(); //FIXME can move all of these to their own folders to make this cleaner
builder.Services.AddScoped<IGetKudosBySenderIdUseCase, GetKudosBySenderIdUseCase>();
builder.Services.AddScoped<IGetKudosByReceiverIdUseCase, GetKudosByReceiverIdUseCase>();
builder.Services.AddScoped<IAddKudoUseCase, AddKudoUseCase>();
builder.Services.AddScoped<IDeleteKudoUseCase, DeleteKudoUseCase>();

builder.Services.AddScoped<IGetCommentsUseCase, GetCommentsUseCase>();
builder.Services.AddScoped<IGetCommentsByKudoIdUseCase, GetCommentsByKudoIdUseCase>();
builder.Services.AddScoped<IAddCommentUseCase, AddCommentUseCase>();
builder.Services.AddScoped<IUpdateCommentUseCase, UpdateCommentUseCase>();
builder.Services.AddScoped<IDeleteCommentUseCase, DeleteCommentUseCase>();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IKudoRepository, KudoRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "employee-recognition", Version = "v1" });

    //Resolve apiDescriptions conflict
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlFilePath))
        c.IncludeXmlComments(xmlFilePath);

    //Settings for token authentication
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter into field the word 'Bearer' following by space and JWT",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:44489")
            //builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowOrigin");

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
    c.InjectStylesheet("/swagger/swagger.css");
    c.InjectJavascript("/swagger/swagger.js");
    c.DefaultModelsExpandDepth(-1);
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
