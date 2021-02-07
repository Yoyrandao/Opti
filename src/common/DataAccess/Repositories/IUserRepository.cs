using DataAccess.Domain;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        void Register(string username);

        User GetById(int id);

        User GetByLogin(string login);
    }
}
