namespace FtpDataAccess.Repositories
{
    public interface IStorageRepository
    {
        void CreateFolder(string name);

        bool IsFolderExists(string name);
    }
}
