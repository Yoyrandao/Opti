using System;

namespace CommonTypes.Configuration
{
    [Serializable]
    public record CertificateConfiguration
    {
        public string StoreName { get; init; }
        
        public string StoreLocation { get; init; }
        
        public string Subject { get; init; }
    }
}