namespace BackgroundAgent.Processing.Models
{
    public record FileSnapshot
    {
        public string Hash { get; set; }

        public string Path { get; set; }

        public string PartName { get; set; }

        public bool Compressed { get; set; }

        public string BaseFileName { get; set; }

        public FileMetaInfo MetaInfo { get; set; }
    }
}