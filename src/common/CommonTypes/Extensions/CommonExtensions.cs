using CommonTypes.Configuration;

namespace CommonTypes.Extensions
{
    public static class CommonExtensions
    {
        public static string Build(this DatabaseConnectionOptions options) =>
            $"Server={options.Host};Port={options.Port};Database={options.Database};User Id={options.Login};Password={options.Password}";
    }
}
