using System;

namespace CommonTypes.Contracts
{
    [Serializable]
    public class Change
    {
        public string BaseFileName { get; init;  }
        
        public string PartName { get; init; }

        public string Base64Content { get; init; }
        
        public bool IsFirst { get; init; }

        public string Hash { get; init; }

        public bool Compressed { get; init; }
    }
}