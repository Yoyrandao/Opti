#nullable enable
using System;

namespace SyncGateway.Exceptions
{
    public class UserFolderNotCreatedException : UserRegistrationException
    {
        public UserFolderNotCreatedException() { }

        public UserFolderNotCreatedException(string? message) : base(message) { }

        public UserFolderNotCreatedException(string? message, Exception? innerException) :
            base(message, innerException) { }
    }
}
