using BackgroundAgent.Processing.Tasks.Processors;

namespace BackgroundAgent.Processing.Tasks
{
    public class CreatedFileOperationTask : OperationTask
    {
        public CreatedFileOperationTask(BasicProcessor[] processors) : base(processors) { }
    }
}