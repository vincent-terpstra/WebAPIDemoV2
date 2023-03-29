using Microsoft.AspNetCore.Mvc;
using WebAPIDemoV2.DataAccess.Interfaces;

namespace WebAPIDemoV2.API;

public class RouteMapper<T>
{
    private readonly string _tags;
    private readonly RouteGroupBuilder _group;


    public RouteMapper(WebApplication app, string route, string tags)
    {
        _tags = tags;
        _group = app.MapGroup(route);
    }

    public RouteMapper<T> MapGetById()
    {
        _group.MapGet("/{id}", Get).WithTags(_tags);
        return this;
    }


    public RouteMapper<T> MapGetAll()
    {
        _group.MapGet("/", GetAll)
            .WithTags(_tags);
        return this;
    }


    public RouteMapper<T> MapPost<TAdd>(Func<TAdd, T> map)
    {
        _group.MapPost("", CreatePostWithMapFunction(map))
            .WithTags(_tags);
        return this;
    }

    public RouteMapper<T> MapDelete()
    {
        _group.MapDelete("/{id}", Delete)
            .WithTags(_tags);
        return this;
    }

    public RouteMapper<T> MapUpdate()
    {
        _group.MapPatch("/{id}", Update)
            .WithTags(_tags);
        return this;
    }


    private T? Get([FromServices] IRepository<T> repo, [FromRoute] int id)
        => repo.Get(id);

    private List<T> GetAll([FromServices] IRepository<T> repo)
        => repo.GetAll();

    private Func<IRepository<T>, TAdd, T> CreatePostWithMapFunction<TAdd>(Func<TAdd, T> map)
        => ([FromServices] repository, [FromBody] add) => repository.Add(map(add));

    private void Delete([FromServices] IRepository<T> repo, [FromRoute] int id)
        => repo.Delete(id);

    private T? Update([FromServices] IRepository<T> repo, [FromRoute] int id, [FromBody] T update)
        => repo.Update(id, update);
}