using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ElCamino.AspNetCore.Identity.AzureTable;
using ElCamino.AspNetCore.Identity.AzureTable.Model;
using Donovan.Game.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using IdentityUser = ElCamino.AspNetCore.Identity.AzureTable.Model.IdentityUser;

namespace Donovan.Game.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure identity support.
            services.AddDefaultIdentity<IdentityUser>(options => { options.SignIn.RequireConfirmedAccount = true; options.User.RequireUniqueEmail = true; })
                .AddAzureTableStores<IdentityDataContext>(new Func<IdentityConfiguration>(() => { return this.GetIdentityConfiguration(); }))
                .AddDefaultTokenProviders()
                .CreateAzureTablesIfNotExists<IdentityDataContext>();

            // Configure Razor support.
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private IdentityConfiguration GetIdentityConfiguration()
        {
            var config = new IdentityConfiguration
            {
                TablePrefix = Configuration["IdentityAzureTable:IdentityConfiguration:TablePrefix"],
                StorageConnectionString = Configuration["IdentityAzureTable:IdentityConfiguration:StorageConnectionString"],
                LocationMode = Configuration["IdentityAzureTable:IdentityConfiguration:LocationMode"],
                IndexTableName = Configuration["IdentityAzureTable:IdentityConfiguration:IndexTableName"],
                RoleTableName = Configuration["IdentityAzureTable:IdentityConfiguration:RoleTableName"],
                UserTableName = Configuration["IdentityAzureTable:IdentityConfiguration:UserTableName"]
            };

            return config;
        }
    }
}
