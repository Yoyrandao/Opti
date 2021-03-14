using System.Collections.Generic;

using DataAccess.Domain;
using DataAccess.Executors;

namespace DataAccess.Repositories
{
    public class FilePartRepository : IFilePartRepository
    {
        public FilePartRepository(ISqlExecutor executor) => _executor = executor;

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
            const string query =
                @"SELECT * FROM public.addFilePart(@FileName, @Base, @Folder, @Hash, @ParentId, @Compressed)";

            return _executor.Get<int>(query,
                new
                {
                    FileName = part.PartName,
                    Base = part.BaseFileName,
                    part.Folder,
                    part.Hash,
                    part.ParentId,
                    part.Compressed
                });
        }

        public void Add(FilePart part)
        {
            const string query =
                @"SELECT * FROM public.addFilePart(@FileName, @Base, @Folder, @Hash, @ParentId, @Compressed)";

            _executor.Get<int>(query,
                new
                {
                    FileName = part.PartName,
                    Base = part.BaseFileName,
                    part.Folder,
                    part.Hash,
                    part.ParentId,
                    part.Compressed
                });
        }

        public void AppendToFile(FilePart part)
        {
            const string query = @"CALL public.appendfilepart(@FileName, @Base, @Folder, @Hash, @Compressed)";
            
            _executor.Execute(query, new
            {
                FileName = part.PartName,
                Base = part.BaseFileName,
                part.Folder,
                part.Hash,
                part.Compressed
            });
        }

        public void UpdateFilePart(string partName, string newHash)
        {
            const string query = @"CALL public.updateFilePart(@FileName, @Hash)";
            
            _executor.Execute(query, new
            {
                FileName = partName,
                Hash = newHash
            });
        }

        public FilePart GetById(int id)
        {
            const string query = @"SELECT fp.id,
                                          fp.folder,
                                          fp.partName,
                                          fp.parentId,
                                          fp.baseFileName,
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
                                          fp.baseFileName,
                                          fp.compressed,
                                          fp.creationTimestamp,
                                          fp.modificationTimestamp
                                   FROM public.fileParts fp
                                   WHERE fp.parentId = @Parent";

            return _executor.Get<FilePart>(query, new { Parent = parentId });
        }

        public FilePart GetByPartName(string partName)
        {
            const string query = @"SELECT fp.id,
                                          fp.folder,
                                          fp.partName,
                                          fp.parentId,
                                          fp.baseFileName,
                                          fp.hash,
                                          fp.compressed,
                                          fp.creationTimestamp,
                                          fp.modificationTimestamp
                                   FROM public.fileParts fp
                                   WHERE fp.partName = @Name";

            return _executor.Get<FilePart>(query, new { Name = partName });
        }

        public IEnumerable<FilePart> GetAll()
        {
            const string query = @"SELECT fp.id,
                                          fp.folder,
                                          fp.partName,
                                          fp.parentId,
                                          fp.baseFileName,
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
                                          fp.baseFileName,
                                          fp.compressed,
                                          fp.creationTimestamp,
                                          fp.modificationTimestamp
                                   FROM public.fileParts fp
                                   WHERE fp.folder LIKE @Folder";

            return _executor.List<FilePart>(query, new { Folder = folder });
        }

        public IEnumerable<FilePart> GetFileByName(string name)
        {
            const string query = @"SELECT fp.id,
                                          fp.folder,
                                          fp.partName,
                                          fp.parentId,
                                          fp.baseFileName,
                                          fp.hash,
                                          fp.compressed,
                                          fp.creationTimestamp,
                                          fp.modificationTimestamp
                                  FROM public.fileParts fp
                                  WHERE fp.baseFileName LIKE @Name";

            return _executor.List<FilePart>(query, new { Name = name });
        }

        #endregion

        private readonly ISqlExecutor _executor;
    }
}