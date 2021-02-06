using DataAccess.Domain;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        User GetById(int id);

        User GetByLogin(string login);
    }
}