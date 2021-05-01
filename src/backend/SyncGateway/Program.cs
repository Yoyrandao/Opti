using CommonTypes.Configuration;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Utils.Certificates;

namespace SyncGateway
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            IConfiguration configuration = null;
            var certificateProvider = new CertificateProvider();
            
            return Host.CreateDefaultBuilder(args)
               .ConfigureLogging(config =>
                {
                    config.ClearProviders();
                })
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

                    configuration = configurationBuilder.Build();

                    if (args != null)
                        configurationBuilder.AddCommandLine(args);
                })
               .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.ConfigureHttpsDefaults(
                            listenOptions =>
                            {
                                var certificateConfiguration = new CertificateSearch(new CertificateConfiguration
                                {
                                    Subject = configuration.GetSection("Certificate")["Subject"],
                                    StoreLocation = configuration.GetSection("Certificate")["StoreLocation"],
                                    StoreName = configuration.GetSection("Certificate")["StoreName"]
                                });
                                
                                listenOptions.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                                listenOptions.ServerCertificate = certificateProvider.GetCertificate(certificateConfiguration);
                            });
                    });
                });
        }
    }
}
