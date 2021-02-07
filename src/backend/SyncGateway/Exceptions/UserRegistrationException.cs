#nullable enable
using System;

namespace SyncGateway.Exceptions
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException() { }

        public UserRegistrationException(string? message) : base(message) { }

        public UserRegistrationException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
