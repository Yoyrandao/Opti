using System.Collections.Generic;

using CommonTypes.Contracts;

namespace BackgroundAgent.Processing.Services
{
    public interface IFileStateService
    {
        List<FileState> GetStateOf(string filename);
    }
}