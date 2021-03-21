namespace BackgroundAgent.Processing.Models
{
    public record FilePart
    {
        public string Path { get; set; }
        
        public string PartName { get; set; }
        
        public string Hash { get; set; }
    }
}