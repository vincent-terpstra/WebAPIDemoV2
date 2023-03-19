using Microsoft.EntityFrameworkCore;
using WebAPIDemoV2.DataAccess.Interfaces;
using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.DataAccess;

public class UsersRepository : IUsersRepository
{
    private readonly WebApiDemoDbContext _context;
    private readonly DbSet<UserModel> _users;

    public UsersRepository(WebApiDemoDbContext context)
    {
        _context = context;
        _users = context.Users;
    }

    public UserModel? Get(int id)
        => _users.FirstOrDefault(user => user.Id == id);

    public List<UserModel> GetAll()
        => _users.ToList();

    public UserModel Add(UserModel model)
    {
        _users.Add(model);
        _context.SaveChanges();

        return model;
    }
}