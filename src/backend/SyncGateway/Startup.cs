using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using SyncGateway.Installers;
using SyncGateway.Services;

namespace SyncGateway
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
               .InstallDataAccess()
               .InstallFtpDataAccess()
               .InstallConfigurationTypes(Configuration)
               .InstallLogic()
               .InstallUtils()
               .InstallShields();

            services.AddHealthChecks()
               .AddCheck<HealthCheckService>("Liveness");

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SyncGateway", Version = "v1" });
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SyncGateway v1"));
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/startup");
                endpoints.MapHealthChecks("/healthz");
                endpoints.MapHealthChecks("/ready");
                endpoints.MapControllers();
            });

            app.UseCors(builder =>
            {
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
            });
        }
    }
}
