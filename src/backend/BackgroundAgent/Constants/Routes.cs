namespace BackgroundAgent.Constants
{
    public static class Routes
    {
        public static string Sync { get; } = "api/filestate";

        public static string CheckCompression { get; } = "/check";

        public static string Update { get; } = "api/update";

        public static string Delete { get; } = "api/delete";
        
        public static string Info { get; } = "api/info/size";
    }
}