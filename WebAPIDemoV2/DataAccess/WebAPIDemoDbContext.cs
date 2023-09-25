using Microsoft.EntityFrameworkCore;
using WebAPIDemoV2.Models;

namespace WebAPIDemoV2.DataAccess;

public sealed class WebApiDemoDbContext : DbContext
{
    public DbSet<User> Users { get; init; } = null!;

    public DbSet<Text> Texts { get; init; } = null!;

    public WebApiDemoDbContext(DbContextOptions<WebApiDemoDbContext> options) : base(options)
    {
            
    }
}