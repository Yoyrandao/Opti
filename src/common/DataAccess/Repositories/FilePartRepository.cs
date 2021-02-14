using System.Collections.Generic;

using DataAccess.Domain;
using DataAccess.Executors;

namespace DataAccess.Repositories
{
    public class FilePartRepository : IFilePartRepository
    {
        public FilePartRepository(ISqlExecutor executor)
        {
            _executor = executor;
        }

        #region Implementation of IFilePartRepository

        public bool IsFilePartExists(string folder, string partName)
        {
            const string query = @"SELECT COUNT(*) 
                                   FROM public.fileParts fp
                                   WHERE fp.folder = @Folder
                                       AND fp.partname = @Name";

            var result = _executor.Get<int>(query, new { Folder = folder, Name = partName });

            return result == 1;
        }

        public int AddAndReturnId(FilePart part)
        {
            const string query = @"SELECT * FROM public.addFilePart(@FileName, @Folder, @ParentId, @Compressed)";

            return _executor.Get<int>(query,
                new { FileName = part.PartName, part.Folder, part.ParentId, part.Compressed });
        }

        public FilePart GetById(int id)
        {
            const string query = @"SELECT fp.id,
                                          fp.folder,
                                          fp.partName,
                                          fp.parentId,
                                          fp.compressed,
                                          fp.creationTimestamp,
                                          fp.modificationTimestamp
                                   FROM public.fileParts fp
                                   WHERE fp.id = @Id";

            return _executor.Get<FilePart>(query, new { Id = id });
        }

        public FilePart GetByParentId(int parentId)
        {
            const string query = @"SELECT fp.id,
                                          fp.folder,
                                          fp.partName,
                                          fp.parentId,
                                          fp.compressed,
                                          fp.creationTimestamp,
                                          fp.modificationTimestamp
                                   FROM public.fileParts fp
                                   WHERE fp.parentId = @Parent";

            return _executor.Get<FilePart>(query, new { Parent = parentId });
        }

        public IEnumerable<FilePart> GetAll()
        {
            const string query = @"SELECT fp.id,
                                          fp.folder,
                                          fp.partName,
                                          fp.parentId,
                                          fp.compressed,
                                          fp.creationTimestamp,
                                          fp.modificationTimestamp
                                   FROM public.fileParts fp";

            return _executor.List<FilePart>(query);
        }

        public IEnumerable<FilePart> GetAllFromFolder(string folder)
        {
            const string query = @"SELECT fp.id,
                                          fp.folder,
                                          fp.partName,
                                          fp.parentId,
                                          fp.compressed,
                                          fp.creationTimestamp,
                                          fp.modificationTimestamp
                                   FROM public.fileParts fp
                                   WHERE fp.folder LIKE @Folder";

            return _executor.List<FilePart>(query, new { Folder = folder });
        }

        #endregion
        
        private readonly ISqlExecutor _executor;
    }
}
