using BackgroundAgent.Processing.Tasks.Processors;

namespace BackgroundAgent.Processing.Tasks
{
    public class ChangedFileOperationTask : OperationTask
    {
        public ChangedFileOperationTask(BasicProcessor[] processors) : base(processors) { }
    }
}