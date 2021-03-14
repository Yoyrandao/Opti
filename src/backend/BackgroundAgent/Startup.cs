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

            services.AddHostedService<Worker>();
        }

        private static IConfiguration _configuration;
    }
}