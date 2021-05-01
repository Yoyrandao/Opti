namespace SyncGateway.Helpers
{
    public static class Routes
    {
        private const string Root = "/api";

        internal static class Fs
        {
            public const string Update = Root + "/update";

            public const string Delete = Root + "/delete";
        }

        internal static class User
        {
            public const string Registration = Root + "/register";
            
            public const string ResourceState = Root + "/filestate";
        }
    }
}
