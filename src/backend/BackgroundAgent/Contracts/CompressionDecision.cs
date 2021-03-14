namespace BackgroundAgent.Contracts
{
    public record CompressionDecision
    {
        public string Name { get; init; }
        
        public bool Decision { get; init; }
    }
}