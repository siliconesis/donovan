using System;
using Donovan.Game.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Donovan.Game.Tests.Core
{
    public class WebHostFixture : IDisposable
    {
        /// <summary>
        /// Gets the test server instance.
        /// </summary>
        /// <remarks>
        /// The test server instance is exposed to callers so that they can create client
        /// instances in a clean state (e.g. default headers).
        /// </remarks>
        public TestServer Server { get; private set; }

        public WebHostFixture()
        {
            // Configure the web host.
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true)
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

            // Create the web server.
            this.Server = host.GetTestServer();
        }

        #region IDisposable

        private bool isDisposed = false;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    this.Server?.Dispose();
                }

                isDisposed = true;
            }
        }

        #endregion
    }
}
