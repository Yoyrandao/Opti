#nullable enable
using System;

namespace SyncGateway.Exceptions
{
    public class UserNotInDatabaseException : UserRegistrationException
    {
        public UserNotInDatabaseException(string username)
        {
            Username = username;
        }

        public UserNotInDatabaseException(string? message, string username) : base(message)
        {
            Username = username;
        }

        public UserNotInDatabaseException(string? message, Exception? innerException, string username) :
            base(message, innerException)
        {
            Username = username;
        }
        
        public string Username { get; }
    }
}
