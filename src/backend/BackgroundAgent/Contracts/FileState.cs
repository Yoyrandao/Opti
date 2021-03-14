namespace BackgroundAgent.Contracts
{
    public record FileState
    {
        public string Hash { get; init; }

        public int ParentId { get; init; }

        public string PartName { get; init; }

        public bool Compressed { get; init; }

        public string BaseFileName { get; init; }
    }
}