namespace BackgroundAgent.Processing.Models
{
    public record FilePart
    {
        public string Path { get; set; }
        
        public string PartName { get; init; }

        public string CompressionHash { get; set; }
        
        public string EncryptionHash { get; set; }
        
        public bool IsFirst { get; set; }
    }
}