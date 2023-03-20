using Microsoft.EntityFrameworkCore;
using WebAPIDemoV2;
using WebAPIDemoV2.Controllers;
using WebAPIDemoV2.Controllers.Commands;
using WebAPIDemoV2.DataAccess;
using WebAPIDemoV2.DataAccess.Interfaces;
using WebAPIDemoV2.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WebApiDemoDbContext>(
    opt => opt.UseSqlite("Data Source=WebApiDemo.db")
);

builder.Services.AddScoped<IRepository<UserModel>, AbstractRepository<UserModel>>();

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

app.MapRoutes<UserModel>("/user", "Users")
    .MapGetAll()
    .MapGetById()
    .MapUpdate()
    .MapDelete()
    .MapPost<AddUser>(
        user => new()
        {
            FirstName = user.FirstName,
            LastName = user.LastName
        });


app.Run();