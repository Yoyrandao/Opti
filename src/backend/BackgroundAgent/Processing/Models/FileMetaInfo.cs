namespace BackgroundAgent.Processing.Models
{
    public record FileMetaInfo
    {
        public string FileName { get; init; }
        
        public int FileSize { get; init; }
        
        public string FileType { get; init; }
        
        public double FileEntropy { get; init; }
    }
}