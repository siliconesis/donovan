using System;
using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Register application services.
            services.AddTransient<IManagerRepository, ManagerRepository>();
            services.AddTransient<IManagerService, ManagerService>();

            // Register configuration options.
            services.Configure<StorageOptions>(this.Configuration.GetSection("ConnectionStrings"));
        }

        /// <summary>
        /// Configures the HTTP request pipeline. The runtime calls this method.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
