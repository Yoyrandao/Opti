using Polly;

namespace Utils.Retrying
{
    public static class RepeaterFactory
    {
        #region Implementation of IRepeaterFactory

        public static IRepeater<T> Create<T>(Policy policy) => new Repeater<T>(policy);

        #endregion
    }
}
