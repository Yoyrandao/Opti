using System;
using System.Collections.Generic;

using Polly;

namespace Utils.Retrying
{
    public static class PolicyFactory
    {
        public static Policy Create<T>(IEnumerable<TimeSpan> sleepDurations)
            where T : Exception =>
            Policy
               .Handle<T>()
               .WaitAndRetry(sleepDurations);
    }
}
