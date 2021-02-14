using System;
using System.Collections.Generic;

namespace SyncGateway.Contracts.Common
{
    [Serializable]
    public record ChangeSet
    {
        public string Identity { get; init; }

        public ICollection<Change> Records { get; set; }
    }
}
