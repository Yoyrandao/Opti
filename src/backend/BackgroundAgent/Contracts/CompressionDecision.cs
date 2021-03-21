using System;

namespace BackgroundAgent.Contracts
{
    [Serializable]
    public record CompressionDecision
    {
        public string Name { get; init; }
        
        public bool Decision { get; init; }
    }
}