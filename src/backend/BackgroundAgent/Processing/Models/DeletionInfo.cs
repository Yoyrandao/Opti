namespace BackgroundAgent.Processing.Models
{
    public record DeletionInfo
    {
        public string Identity { get; set; }
        
        public string Filename { get; set; }
    }
}