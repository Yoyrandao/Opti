namespace BackgroundAgent.Processing.Models
{
    public record FsEvent
    {
        public string Name { get; init; }
        
        public string FilePath { get; init; }
    }
}