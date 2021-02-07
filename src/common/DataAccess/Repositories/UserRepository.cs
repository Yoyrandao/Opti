using DataAccess.Domain;
using DataAccess.Executors;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlExecutor _executor;

        public UserRepository(ISqlExecutor executor)
        {
            _executor = executor;
        }

        #region Implementation of IUserRepository

        public void Register(string username)
        {
            const string query = @"CALL public.registerUser(@Login, @Folder)";

            _executor.Execute(query, new { Login = username, Folder = $"/{username}" });
        }

        public User GetById(int id)
        {
            const string query = @"SELECT u.id,
                                          u.accountUid,
                                          u.login,
                                          u.folder,
                                          u.creationTimestamp,
                                          u.modificationTimestamp
                                   FROM public.user u
                                   WHERE u.id = @Id";

            return _executor.Get<User>(query, new { Id = id });
        }

        public User GetByLogin(string login)
        {
            const string query = @"SELECT u.id,
                                          u.accountUid,
                                          u.login,
                                          u.folder,
                                          u.creationTimestamp,
                                          u.modificationTimestamp
                                   FROM public.user u
                                   WHERE u.login LIKE @Login";

            return _executor.Get<User>(query, new { Login = login });
        }

        #endregion
    }
}
