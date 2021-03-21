using System;
using System.IO;

namespace BackgroundAgent.Constants
{
    public static class FsLocation
    {
        private static string UserHome => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public static string ApplicationRoot => Path.Combine(UserHome, ".opti");

        public static string ApplicationData => Path.Combine(ApplicationRoot, "data");

        public static string ApplicationTempData => Path.Combine(ApplicationRoot, "temp");

        public static string ApplicationPrivateKey => Path.Combine(ApplicationRoot, "p");

        public static string ApplicationEncryptionKey => Path.Combine(ApplicationRoot, "s");

        public static string ApplicationEncryptionIv => Path.Combine(ApplicationRoot, "iv");
    }
}