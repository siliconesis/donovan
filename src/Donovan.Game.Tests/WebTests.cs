using System;
using System.Threading.Tasks;
using Xunit;
using Donovan.Game.Tests.Core;

namespace Donovan.Game.Tests
{
    [Collection("TestEnvironment")]
    public class WebTests
    {
        private WebHostFixture Fixture { get; }

        public WebTests(WebHostFixture fixture)
        {
            this.Fixture = fixture;
        }

        [Fact]
        public async Task PageIsUpHome()
        {
            var client = this.Fixture.Server.CreateClient();
            var response = await client.GetAsync(client.BaseAddress);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
