using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

            // Get token signing key for authentication service.
            var options = Options.Create(
                this.Configuration.GetSection("ConnectionStrings")
                    .Get<StorageOptions>());

            var key = new ConfigurationRepository(options)
                .GetSettingAsync("Token", "SigningKey")
                .Result
                .Value;

            var keyBytes = Encoding.ASCII.GetBytes(key);

            // Register authentication services.
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Register application services.
            services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IManagerService, ManagerService>();
            services.AddTransient<IManagerRepository, ManagerRepository>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
