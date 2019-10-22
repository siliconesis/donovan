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

namespace Donovan.Tests
{
    public class WebApiHostFixture : IDisposable
    {
        public WebApiHostFixture()
        {
            // Configure the web host.
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = WebHost.CreateDefaultBuilder()
                .UseConfiguration(config)
                .UseStartup<Startup>();

            // Create the web server.
            this.Server = new TestServer(builder);
            this.Client = this.Server.CreateClient();
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

        public HttpClient Client { get; private set; }

        public TestServer Server { get; private set; }
    }
}
