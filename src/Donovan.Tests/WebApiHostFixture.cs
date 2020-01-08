using System;
using System.Net;
using System.Net.Http;
using Donovan;
using Donovan.Server;
using Donovan.Server.Web;
using Donovan.Server.Web.Api;
using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Donovan.Tests
{
    public class WebApiHostFixture : IDisposable
    {
        public HttpClient Client { get; private set; }

        public TestServer Server { get; private set; }

        public WebApiHostFixture()
        {
            // Configure the web host.
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new HostBuilder()
                .ConfigureWebHost(web =>
                {
                    web
                        .UseConfiguration(config)
                        .UseStartup<Startup>()
                        .UseTestServer();
                });

            var host = builder.Start();

            // Create the web server and client.
            this.Server = host.GetTestServer();
            this.Client = host.GetTestClient();
        }

        #region IDisposable

        private bool isDisposed = false;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    this.Client?.Dispose();
                    this.Server?.Dispose();
                }

                isDisposed = true;
            }
        }

        #endregion
    }
}
