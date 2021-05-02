#nullable enable
using System;

using CommonTypes.Programmability;

namespace BackgroundAgent.Processing.Models
{
    public sealed record FsEvent : BaseEvent
    {
        public string? Name { get; init; }

        public string FilePath { get; init; }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Name, FilePath);

        public bool Equals(FsEvent? other) => base.Equals(other);
    }
}