namespace BackgroundAgent.Configuration
{
    public record EndpointConfiguration
    {
        public string Backend { get; init; }
        
        public string CompressionChecker { get; init; }
    }
}