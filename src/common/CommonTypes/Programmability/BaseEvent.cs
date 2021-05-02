#nullable enable
using System;

namespace CommonTypes.Programmability
{
    public record BaseEvent
    {
        public DateTime EventTimestamp { get; init; }

        public override int GetHashCode() => EventTimestamp.GetHashCode();

        public virtual bool Equals(BaseEvent? other)
        {
            if (other == null)
                return false;

            return EventTimestamp - other.EventTimestamp < TimeSpan.FromMilliseconds(500);
        }
    }
}