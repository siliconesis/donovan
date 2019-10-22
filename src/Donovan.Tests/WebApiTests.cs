using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Donovan;
using Donovan.Models;
using Xunit;

namespace Donovan.Tests
{
    public class WebApiTests : IClassFixture<WebApiHostFixture>
    {
        private readonly WebApiHostFixture fixture;

        public WebApiTests(WebApiHostFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ManagersGetFromApi()
        {
            var uri = "/api/managers/ZnJlZEBiZWRyb2NrLmNvbQ==";     // fred@bedrock.com

            using (var response = await this.fixture.Client.GetAsync(uri))
            {
                response.EnsureSuccessStatusCode();

                var manager = await response.Content.ReadAsAsync<Manager>();

                Assert.Equal("Fred Flintstone", manager.Name);
            }
        }
    }
}
