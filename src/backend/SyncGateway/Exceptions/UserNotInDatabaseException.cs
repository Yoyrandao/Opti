#nullable enable
using System;

namespace SyncGateway.Exceptions
{
    public class UserNotInDatabaseException : UserRegistrationException
    {
        public UserNotInDatabaseException() { }

        public UserNotInDatabaseException(string? message) : base(message) { }

        public UserNotInDatabaseException(string? message, Exception? innerException) :
            base(message, innerException) { }
    }
}
