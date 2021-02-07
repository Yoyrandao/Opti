using System;

namespace Utils.Retrying
{
    public interface IRepeater<T>
    {
        void Process(Action action);
    }
}
