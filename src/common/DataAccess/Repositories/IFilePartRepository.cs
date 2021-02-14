using System.Collections.Generic;

using DataAccess.Domain;

namespace DataAccess.Repositories
{
    public interface IFilePartRepository
    {
        bool IsFilePartExists(string folder, string partName);

        int AddAndReturnId(FilePart part);

        FilePart GetById(int id);

        FilePart GetByParentId(int parentId);

        IEnumerable<FilePart> GetAll();

        IEnumerable<FilePart> GetAllFromFolder(string folder);
    }
}
