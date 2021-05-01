using BackgroundAgent.Processing.Tasks.Processors;

namespace BackgroundAgent.Processing.Tasks
{
    public class DeletedFileOperationTask : OperationTask
    {
        public DeletedFileOperationTask(BasicProcessor[] processors) : base(processors) { }
    }
}