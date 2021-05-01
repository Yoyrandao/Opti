using System;
using System.Security.Cryptography.X509Certificates;

using CommonTypes.Configuration;

namespace Utils.Certificates
{
    public record CertificateSearch
    {
        public CertificateSearch(CertificateConfiguration configuration)
        {
            StoreName = Enum.Parse<StoreName>(configuration.StoreName);
            StoreLocation = Enum.Parse<StoreLocation>(configuration.StoreLocation);
            Subject = configuration.Subject;
        }
        
        public StoreName StoreName { get; }

        public StoreLocation StoreLocation { get; }

        public string Subject { get; }
    }
}