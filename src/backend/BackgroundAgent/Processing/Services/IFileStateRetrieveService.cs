using System.Collections.Generic;

using BackgroundAgent.Contracts;
using BackgroundAgent.Processing.Models;

using CommonTypes.Contracts;

namespace BackgroundAgent.Processing.Services
{
    public interface IFileStateRetrieveService
    {
        ICollection<FileState> ApplyChangeEvent(FsEvent @event);
    }
}