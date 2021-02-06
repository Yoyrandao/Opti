using System;

namespace DataAccess.Domain
{
    public record BaseEntity
    {
        public int Id { get; init; }
        
        public DateTime CreationTimestamp { get; init; }
        
        public DateTime ModificationTimestamp { get; init; }
    }
}