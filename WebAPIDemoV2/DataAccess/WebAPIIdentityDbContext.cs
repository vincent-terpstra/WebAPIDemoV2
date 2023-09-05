using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPIDemoV2.Models;

namespace WebAPIDemoV2.DataAccess;

//dotnet ef migrations add IdentityCreate --context WebAPIIdentityDbContext
//dotnet ef database update --context WebAPIIDentityDbContext
public class WebApiIdentityDbContext : IdentityDbContext<MyUser>
{
    public WebApiIdentityDbContext(DbContextOptions<WebApiIdentityDbContext> options) : base(options) { }
}