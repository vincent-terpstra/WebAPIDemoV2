using Microsoft.EntityFrameworkCore;
using WebAPIDemoV2.DataAccess;
using WebAPIDemoV2.DataAccess.Interfaces;
using WebAPIDemoV2.Models;

//[assembly: InternalsVisibleTo("WebAPIDemoV2.Tests")]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Add repositories to the application
builder.Services.AddScoped<IRepository<User>, AbstractRepository<User>>();
builder.Services.AddScoped<IRepository<Text>, AbstractRepository<Text>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WebApiDemoDbContext>(
    opt => opt.UseSqlite("Data Source=WebApiDemo.db")
);



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

// app.MapRoutes<User>("/users", "Users")
//     .MapGetAll()
//     .MapGetById()
//     .MapUpdate()
//     .MapDelete()
//     .MapPost<AddUser>(
//         user => new()
//         {
//             FirstName = user.FirstName,
//             LastName = user.LastName
//         });
app.Run();

public partial class Program { }