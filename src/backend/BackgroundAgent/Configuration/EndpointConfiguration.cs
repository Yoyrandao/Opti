using System;

namespace BackgroundAgent.Configuration
{
    [Serializable]
    public record EndpointConfiguration
    {
        public string Backend { get; init; }
        
        public string CompressionChecker { get; init; }
    }
}