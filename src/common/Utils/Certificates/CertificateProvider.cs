using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Utils.Certificates
{
    public class CertificateProvider : ICertificateProvider
    {
        public X509Certificate2 GetCertificate(CertificateSearch search)
        {
            var store = new X509Store(search.StoreName, search.StoreLocation);
            store.Open(OpenFlags.ReadOnly);

            var returnCollection = new List<X509Certificate2>();
            foreach (var certificate in store.Certificates)
            {
                if (certificate.Subject.Contains(search.Subject, StringComparison.InvariantCulture))
                {
                    returnCollection.Add(certificate);
                }
            }

            return returnCollection.Single();
        }
    }
}