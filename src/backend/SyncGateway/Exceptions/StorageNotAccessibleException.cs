#nullable enable
using System;

namespace SyncGateway.Exceptions
{
    public class StorageNotAccessibleException : Exception
    {
        public StorageNotAccessibleException() { }

        public StorageNotAccessibleException(string? message) : base(message) { }

        public StorageNotAccessibleException(string? message, Exception? innerException) :
            base(message, innerException) { }
    }
}
