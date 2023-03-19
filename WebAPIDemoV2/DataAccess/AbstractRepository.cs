using Microsoft.EntityFrameworkCore;
using WebAPIDemoV2.DataAccess.Interfaces;
using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.DataAccess;

public class AbstractRepository<T> : IRepository<T> where T : BaseModel
{
    private readonly DbContext _context;
    private readonly DbSet<T> _models;

    public AbstractRepository(WebApiDemoDbContext context)
    {
        _context = context;
        _models = context.Set<T>();
    }

    public List<T> GetAll()
        => _models.AsNoTracking().ToList();

    public T? Get(int id)
        => _models.AsNoTracking().FirstOrDefault(m => m.Id == id);

    public T Add(T model)
    {
        _models.Add(model);
        _context.SaveChanges();
        return model;
    }
}