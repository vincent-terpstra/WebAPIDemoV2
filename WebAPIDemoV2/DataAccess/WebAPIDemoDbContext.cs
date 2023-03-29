using Microsoft.EntityFrameworkCore;
using WebAPIDemoV2.Models;

namespace WebAPIDemoV2.DataAccess;

public class WebApiDemoDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Text> Texts { get; set; } = null!;

    public WebApiDemoDbContext(DbContextOptions<WebApiDemoDbContext> options) : base(options)
    {
            
    }
}