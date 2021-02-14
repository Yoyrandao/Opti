using System;

using Polly;

namespace Utils.Retrying
{
    public class Repeater<T> : IRepeater<T>
    {
        public Repeater(Policy policy)
        {
            _policy = policy;
        }

        #region Implementation of IRepeater

        public void Process(Action action)
        {
            _policy.Execute(action);
        }

        #endregion
        
        private readonly Policy _policy;
    }
}
