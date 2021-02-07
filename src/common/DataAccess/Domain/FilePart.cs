namespace DataAccess.Domain
{
    public record FilePart : BaseEntity
    {
        public string Folder { get; init; }

        public string PartName { get; init; }

        public string Hash { get; init; }

        public int? ParentId { get; init; }

        public bool Compressed { get; init; }
    }
}
