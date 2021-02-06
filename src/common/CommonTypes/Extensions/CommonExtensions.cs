using CommonTypes.Configuration;

namespace CommonTypes.Extensions
{
    public static class CommonExtensions
    {
        public static string Build(this DatabaseConnectionSettings settings)
        {
            return
                $"Server={settings.Host};Port={settings.Port};Database={settings.Database};User Id={settings.Login};Password={settings.Password}";
        }
    }
}