using Microsoft.AspNetCore.Mvc;
using WebAPIDemoV2.DataAccess.Interfaces;
using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.API.Abstractions;

[ApiController]
[Route("[controller]")]
public class AbstractController<T> : ControllerBase where T : BaseModel
{
    protected readonly IRepository<T> Repository;

    public AbstractController(IRepository<T> repository)
    {
        Repository = repository;
    }

    [HttpGet]
    public ActionResult<List<T>> GetAll()
        => Repository.GetAll();

    [HttpGet("{id}")]
    public ActionResult<T?> Get(int id)
    {
        T? result = Repository.Get(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public ActionResult<T> Post(T entity)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.Values);
        var added = Repository.Add(entity);
        
        return CreatedAtAction("Get", new { added.Id }, added);
    }

    [HttpPatch("{id}")]
    public ActionResult<T> Patch(int id, T entity)
    {
        return Ok(Repository.Update(id, entity));
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        Repository.Delete(id);
        return NoContent();
    }
}