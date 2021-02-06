namespace DataAccess.Domain
{
    public record FilePart : BaseEntity
    {
        public string PartName { get; init; }

        public int? ParentId { get; init; }

        public bool Compressed { get; init; }
    }
}