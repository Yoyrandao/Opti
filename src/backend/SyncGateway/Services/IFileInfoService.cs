namespace SyncGateway.Services
{
    public interface IFileInfoService
    {
        long GetSize(string filename, string identity);
    }
}