namespace BackgroundAgent.Processing.Models
{
    public record FilePart
    {
        public string Path { get; init; }
        
        public string PartName { get; init; }
        
        public string Hash { get; init; }
        
        public bool IsFirst { get; set; }
    }
}