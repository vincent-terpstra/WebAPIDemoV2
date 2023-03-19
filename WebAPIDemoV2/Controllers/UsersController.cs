using Microsoft.AspNetCore.Mvc;
using WebAPIDemoV2.DataAccess.Interfaces;
using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.Controllers;

public class UsersController
{
    public void MapRoutes(WebApplication app)
    {
        app.MapGet("/users/{id}", GetUser);
        app.MapGet("/users", GetAllUsers);
        app.MapPost("/users", AddUser);
    }

    UserModel? GetUser([FromServices] IUsersRepository repo, [FromRoute] int id)
        => repo.Get(id);

    List<UserModel> GetAllUsers([FromServices] IUsersRepository repo)
        => repo.GetAll();

    UserModel AddUser([FromServices] IUsersRepository repo, [FromBody] UserModel adduser)
        => repo.Add(adduser);

}