#nullable enable
using System;

namespace SyncGateway.Exceptions
{
    public class UserFolderNotCreatedException : UserRegistrationException
    {
        public UserFolderNotCreatedException(string username)
        {
            Username = username;
        }

        public UserFolderNotCreatedException(string? message, string username) : base(message)
        {
            Username = username;
        }

        public UserFolderNotCreatedException(string? message, Exception? innerException, string username) :
            base(message, innerException)
        {
            Username = username;
        }
        
        public string Username { get; }
    }
}
