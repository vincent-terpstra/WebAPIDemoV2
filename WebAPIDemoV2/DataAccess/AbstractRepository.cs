using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebAPIDemoV2.DataAccess.Interfaces;
using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.DataAccess;

public class AbstractRepository<T> : IRepository<T> where T : BaseModel, new()
{
    private readonly DbContext _context;
    private readonly DbSet<T> _models;

    public AbstractRepository(WebApiDemoDbContext context)
    {
        _context = context;
        _models = context.Set<T>();
    }

    public void SaveChanges() 
        => _context.SaveChanges();

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

    public void Delete(int id)
    {
        try
        {
            _models.Remove(new() {Id = id});
            _context.SaveChanges();
        }
        catch
        {
            // ignored
        }
    }

    private readonly IEnumerable<PropertyInfo> _properties 
        = typeof(T).GetProperties().Where(p => p.CanWrite);
    
    public T? Update(int id, T update)
    {
        update.Id = id;
        var model = _models.FirstOrDefault(m => m.Id == id);

        if (model is not null)
        {
            foreach (PropertyInfo prop in _properties)
            {
                var value = prop.GetValue(update);
                if (value is not null)
                    prop.SetValue(model, value);
            }
            _context.SaveChanges();
        }
        return model;
    }
}