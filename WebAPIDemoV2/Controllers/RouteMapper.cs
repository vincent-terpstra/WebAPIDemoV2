using Microsoft.AspNetCore.Mvc;
using WebAPIDemoV2.DataAccess.Interfaces;

namespace WebAPIDemoV2.Controllers;

public class RouteMapper<T>
{
    private readonly WebApplication _app;
    private readonly string _route;
    private readonly string _tags;

    public RouteMapper(WebApplication app, string route, string tags)
    {
        _app = app;
        _route = route;
        _tags = tags;
    }

    public RouteMapper<T> MapGetById()
    {
        _app.MapGet(@$"{_route}/{{id}}", Get)
            .WithTags(_tags);
        return this;
    }


    public RouteMapper<T> MapGetAll()
    {
        _app.MapGet(@$"{_route}s", GetAll)
            .WithTags(_tags);
        return this;
    }


    public RouteMapper<T> MapPost<TAdd>(Func<TAdd, T> map)
    {
        _app.MapPost($@"{_route}", CreatePostWithMapFunction(map))
            .WithTags(_tags);
        return this;
    }


    private T? Get([FromServices] IRepository<T> repo, [FromRoute] int id)
        => repo.Get(id);

    private List<T> GetAll([FromServices] IRepository<T> repo)
        => repo.GetAll();

    private Func<IRepository<T>, TAdd, T> CreatePostWithMapFunction<TAdd>(Func<TAdd, T> map)
        => ([FromServices] repository, [FromBody] add) => repository.Add(map(add));
}