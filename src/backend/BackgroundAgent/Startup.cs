using BackgroundAgent.Installers;
using BackgroundAgent.Workers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace BackgroundAgent
{
    public static class Startup
    {
        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            _configuration = context.Configuration;
            
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(_configuration, "Serilog")
               .CreateLogger();

            Log.Logger.Information("Application started.");
            
            services
               .InstallLogic()
               .InstallUtils()
               .InstallConfiguration(_configuration);
            services.AddHostedService<InitializationWorker>();
        }

        private static IConfiguration _configuration;
    }
}