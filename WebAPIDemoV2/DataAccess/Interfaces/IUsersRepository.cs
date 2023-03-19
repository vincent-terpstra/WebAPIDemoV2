using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.DataAccess.Interfaces;

public interface IUsersRepository
{
    public UserModel? Get(int id);

    public List<UserModel> GetAll();

    public UserModel Add(UserModel model);
}