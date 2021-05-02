using System.Threading;

namespace BackgroundAgent.Processing.EventHandling
{
    public interface IEventHandler
    {
        void Prepare(CancellationToken token);
    }
}