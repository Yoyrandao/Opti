namespace CommonTypes.Contracts
{
    public record DeleteSet
    {
        public string Identity { get; init; }
        
        public string Filename { get; init; }
    }
}