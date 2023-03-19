using Microsoft.EntityFrameworkCore;
using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.DataAccess;

public class WebApiDemoDbContext : DbContext
{
    public DbSet<UserModel> Users { get; set; } = null!;

    public WebApiDemoDbContext(DbContextOptions<WebApiDemoDbContext> options) : base(options)
    {
            
    }
}