using FluentFTP;

namespace FtpDataAccess.Factories
{
    public interface IFtpConnectionFactory
    {
        IFtpClient Create();
    }
}
