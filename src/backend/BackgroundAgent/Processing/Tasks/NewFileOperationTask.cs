using BackgroundAgent.Processing.Tasks.Processors;

namespace BackgroundAgent.Processing.Tasks
{
    public class NewFileOperationTask : OperationTask
    {
        public NewFileOperationTask(BasicProcessor[] processors) : base(processors) { }
    }
}