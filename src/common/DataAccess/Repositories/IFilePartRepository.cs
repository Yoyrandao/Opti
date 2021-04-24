using System.Collections.Generic;

using DataAccess.Domain;

namespace DataAccess.Repositories
{
    public interface IFilePartRepository
    {
        bool IsFilePartExists(string folder, string partName);

        void Add(FilePart part);

        int AddAndReturnId(FilePart part);

        void AppendToFile(FilePart part);

        void UpdateFilePart(string partName, string newCompressionHash, string newEncryptionHash);

        FilePart GetById(int id);

        FilePart GetByParentId(int parentId);

        FilePart GetByPartName(string partName);

        IEnumerable<FilePart> GetAll();

        IEnumerable<FilePart> GetAllFromFolder(string folder);

        IEnumerable<FilePart> GetFileByName(string name);
    }
}