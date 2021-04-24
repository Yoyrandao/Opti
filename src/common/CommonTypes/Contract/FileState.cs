namespace CommonTypes.Contracts
{
    public record FileState
    {
        public string CompressionHash { get; init; }
        
        public string EncryptionHash { get; init; }

        public int ParentId { get; init; }

        public string PartName { get; init; }

        public bool Compressed { get; init; }

        public string BaseFileName { get; init; }
    }
}