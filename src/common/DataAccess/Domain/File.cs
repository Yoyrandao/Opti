using System.Collections.Generic;

namespace DataAccess.Domain
{
    public record File
    {
        public IList<FilePart> Parts { get; init; } = new List<FilePart>();
    }
}