using Microsoft.AspNetCore.Mvc;
using WebAPIDemoV2.Controllers.Commands;
using WebAPIDemoV2.DataAccess.Interfaces;
using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.Controllers;

public class UsersController
{
    public void MapRoutes(WebApplication app)
    {
        app.MapPost("/users", AddUser);
    }

    UserModel? GetUser([FromServices] IRepository<UserModel> repo, [FromRoute] int id)
        => repo.Get(id);

    List<UserModel> GetAllUsers([FromServices] IRepository<UserModel> repo)
        => repo.GetAll();

    UserModel AddUser([FromServices] IRepository<UserModel> repo, [FromBody] AddUser adduser)
        => repo.Add(new UserModel()
        {
            FirstName = adduser.FirstName,
            LastName = adduser.LastName
        });

}