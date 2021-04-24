using System;

namespace DataAccess.Domain
{
    [Serializable]
    public record FilePart : BaseEntity
    {
        public string Folder { get; set; }

        public string PartName { get; init; }

        public string BaseFileName { get; init; }

        public string CompressionHash { get; init; }
        
        public string EncryptionHash { get; init; }

        public int? ParentId { get; init; }

        public bool Compressed { get; init; }
    }
}