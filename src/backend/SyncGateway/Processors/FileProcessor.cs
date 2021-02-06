using System.Collections.Generic;
using System.Linq;

using DataAccess.Domain;
using DataAccess.Repositories;

namespace SyncGateway.Processors
{
    public class FileProcessor : IFileProcessor
    {
        public FileProcessor(IFilePartRepository filePartRepository)
        {
            _filePartRepository = filePartRepository;
        }

        #region Implementation of IFileProcessor

        public IEnumerable<File> GetFilesFromFolder(string folder)
        {
            var files = new List<File>();

            var fileParts = _filePartRepository.GetAllFromFolder(folder);
            var filePointers = fileParts.Where(x => !x.ParentId.HasValue).ToList();

            foreach (var filePointer in filePointers)
            {
                var current = filePointer;
                var file = new File();

                file.Parts.Add(filePointer);

                while (current != null)
                {
                    current = _filePartRepository.GetByParentId(current.Id);

                    if (current == null)
                    {
                        break;
                    }

                    file.Parts.Add(current);
                }

                files.Add(file);
            }

            return files;
        }

        #endregion

        private readonly IFilePartRepository _filePartRepository;
    }
}