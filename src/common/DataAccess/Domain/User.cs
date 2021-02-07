using System;

namespace DataAccess.Domain
{
    public record User : BaseEntity
    {
        public Guid AccountUid { get; init; }

        public string Login { get; init; }

        public string Folder { get; init; }
    }
}
