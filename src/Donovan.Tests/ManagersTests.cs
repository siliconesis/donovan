using System;
using System.Threading;
using System.Threading.Tasks;
using Donovan;
using Donovan.Client;
using Xunit;

namespace Donovan.Tests
{
    /// <summary>
    /// Contains tests for managers.
    /// </summary>
    public class ManagersTests : IClassFixture<WebApiHostFixture>
    {
        private WebApiHostFixture Fixture { get; }

        public ManagersTests(WebApiHostFixture fixture)
        {
            this.Fixture = fixture;
        }

        [Fact(Skip = "Not Implemented")]
        public async Task ManagersGet()
        {
        }

        [Fact(Skip = "Debugging")]
        public async Task ManagersGetById()
        {
            var email = "fred@bedrock.com";

            var client = GameClientFactory.Create(this.Fixture.Client);
            var manager = await client.GetManagerAsync(email);

            Assert.Equal("fred@bedrock.com", manager.Email);
            Assert.Equal("Fred Flintstone", manager.Name);
            Assert.Equal("Microsoft", manager.Provider);
        }

        [Fact(Skip = "Not Implemented")]
        public async Task ManagersGetByIdNotFound()
        {
            var email = "george@orbitcity.com";
        }

        [Fact(Skip = "Not Implemented")]
        public async Task ManagersGetByIdUnauthorized()
        {
        }

        [Fact(Skip = "Debugging")]
        public async Task ManagersRegister()
        {
            var client = GameClientFactory.Create(this.Fixture.Client);
        }

        [Fact(Skip = "Not Implemented")]
        public async Task ManagersRegisterAlreadyRegistered()
        {
        }
    }
}
