using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPIDemoV2.API.MinimalAPI;
using WebAPIDemoV2.DataAccess;
using WebAPIDemoV2.DataAccess.Interfaces;
using WebAPIDemoV2.Models;

//[assembly: InternalsVisibleTo("WebAPIDemoV2.Tests")]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Add repositories to the application
builder.Services.AddScoped(typeof(IRepository<>), typeof(AbstractRepository<>));

// Add user and Authentication
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WebApiDemoDbContext>(
    opt => opt.UseSqlite("Data Source=WebApiDemo.db")
);

builder.Services.AddDbContext<WebApiIdentityDbContext>(x => x.UseSqlite("Data Source=Identity.db"));
builder.Services.AddIdentityCore<MyUser>()
    .AddEntityFrameworkStores<WebApiIdentityDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<MyUser>();

app.MapRoutes<User>("/users", "Users")
    .MapGetAll()
    .MapGetById()
    .MapUpdate()
    .MapDelete()
    .MapPost();

app.MapGet("/hello", () => "World")
    .RequireAuthorization();

app.Run();

public partial class Program { }