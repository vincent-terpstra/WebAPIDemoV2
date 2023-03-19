using Microsoft.AspNetCore.Mvc;
using WebAPIDemoV2.DataAccess.Interfaces;

namespace WebAPIDemoV2.Controllers;

public class RouteMapper<T>
{
    private readonly WebApplication _app;
    private readonly string _route;

    public RouteMapper(WebApplication app, string route)
    {
        _app = app;
        _route = route;
    }

    public RouteMapper<T> MapGetById()
    {
        _app.MapGet(@$"{_route}/{{id}}", Get);
        return this;
    }

    public RouteMapper<T> MapGetAll()
    {
        _app.MapGet(@$"{_route}s", GetAll);
        return this;
    }

    public RouteMapper<T> MapPost<TAdd>()
    {
        _app.MapPost($@"{_route}", Add<TAdd> );
        return this;
    }

    T? Get([FromServices] IRepository<T> repo, [FromRoute] int id)
        => repo.Get(id);

    List<T> GetAll([FromServices] IRepository<T> repo)
        => repo.GetAll();
    
    T Add<TAdd>([FromServices] IRepository<T> repo, [FromBody] TAdd adduser, Func<TAdd, T> map)
        => repo.Add(map(adduser));
}