using System.Collections.Generic;

namespace BackgroundAgent.Processing.Models
{
    public record FileSnapshot
    {
        public string Path { get; set; }

        public string CompressedPath { get; set; }

        public string DecryptedPath { get; set; }

        public List<FilePart> Parts { get; set; }

        public bool Compressed { get; set; }

        public string BaseFileName { get; set; }

        public FileMetaInfo MetaInfo { get; set; }
    }
}