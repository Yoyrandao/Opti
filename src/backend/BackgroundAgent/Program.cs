using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundAgent
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    configurationBuilder.Sources.Clear();

                    configurationBuilder
                       .SetBasePath(context.HostingEnvironment.ContentRootPath)
                       .AddJsonFile("appsettings.json", true, true)
                       .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true,
                            true)
                       .AddJsonFile($"secrets/appsettings.{context.HostingEnvironment.EnvironmentName}.json", true,
                            true)
                       .AddEnvironmentVariables();

                    if (args != null)
                        configurationBuilder.AddCommandLine(args);
                })
               .ConfigureLogging(config => { config.ClearProviders(); })
               .ConfigureServices(Startup.ConfigureServices);
    }
}