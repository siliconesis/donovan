using System;
using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Donovan;
using Donovan.Server;
using Donovan.Server.Repositories;
using Donovan.Server.Services;
using Donovan.Server.Storage;

namespace Donovan.Server.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures services for the application container. The runtime calls this method.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // Register framework services.
            services.AddControllers();

            // Register application services.
            services.AddTransient<IManagerRepository, ManagerRepository>();
            services.AddTransient<IManagerService, ManagerService>();

            // Register configuration options.
            services.Configure<StorageOptions>(this.Configuration.GetSection("ConnectionStrings"));
        }

        /// <summary>
        /// Configures the HTTP request pipeline. The runtime calls this method.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
