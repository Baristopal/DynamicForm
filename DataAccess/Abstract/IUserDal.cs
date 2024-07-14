using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IUserDal : IRepositoryBase<User>
{
    Task<User> GetUserByEmail(string email);
    Task<User> IsExistUserByUserName(string userName);
}
