using System.Collections.Generic;

using BackgroundAgent.Contracts;
using BackgroundAgent.Processing.Models;

namespace BackgroundAgent.Processing.Services
{
    public interface IFileStateRetrieveService
    {
        ICollection<FileState> ApplyChangeEvent(FsEvent @event);
    }
}