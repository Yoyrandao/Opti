using System;

namespace SyncGateway.Exceptions.Shields
{
    public interface IExceptionShield<T>
    {
        T Protect(Func<T> func);
    }
}
