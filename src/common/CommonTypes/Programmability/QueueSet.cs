#nullable enable
#pragma warning disable 8618

using System.Collections;

namespace CommonTypes.Programmability
{
    public class QueueSet<T>: Queue where T: class
    {
        public delegate void EnqueueEventHandler();

        public event EnqueueEventHandler OnPush;
        
        public override void Enqueue(object? obj)
        {
            if (Contains(obj))
                return;
            
            base.Enqueue(obj);
        }

        public void Push(T @object)
        {
            Enqueue(@object);
            OnPush();
        }

        public T? Pop()
        {
            return base.Dequeue() as T;
        }
    }
}