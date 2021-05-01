using System.Security.Cryptography.X509Certificates;

namespace Utils.Certificates
{
    public interface ICertificateProvider
    {
        X509Certificate2 GetCertificate(CertificateSearch search);
    }
}