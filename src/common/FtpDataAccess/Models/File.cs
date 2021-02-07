namespace FtpDataAccess.Models
{
    public record File
    {
        public string Folder { get; init; }

        public string Name { get; init; }
    }
}
