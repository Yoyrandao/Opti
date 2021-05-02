#nullable enable

using System.Collections;

namespace CommonTypes.Programmability
{
    public class QueueSet<T> : Queue where T : BaseEvent
    {
        public override void Enqueue(object? obj)
        {
            if (Contains(obj))
                return;

            base.Enqueue(obj);
        }

        public void Push(T @object)
        {
            Enqueue(@object);
        }

        public T? Pop()
        {
            if (Count == 0)
                return null;

            return base.Dequeue() as T;
        }

        public T? Check()
        {
            if (Count == 0)
                return null;

            return base.Peek() as T;
        }
    }
}