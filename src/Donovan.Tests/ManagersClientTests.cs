using System;
using System.Threading.Tasks;
using Xunit;
using Donovan.Client;
using Donovan.Tests.Core;

namespace Donovan.Tests
{
    public class ManagersClientTests
    {
        private WebHostFixture Fixture { get; }

        public ManagersClientTests(WebHostFixture fixture)
        {
            this.Fixture = fixture;
        }

        [Fact(Skip = "Debugging")]
        public async Task ManagersGetById()
        {
            var email = "fred@bedrock.com";

            var client = GameClientFactory.Create(this.Fixture.Server.CreateClient());
            var manager = await client.GetManagerAsync(email);

            Assert.Equal("fred@bedrock.com", manager.Email);
            Assert.Equal("Fred Flintstone", manager.Name);
        }

        [Fact(Skip = "Not Implemented")]
        public async Task ManagersGetByIdNotFound()
        {
            var email = "george@orbitcity.com";

            throw new NotImplementedException();
        }

        [Fact(Skip = "Not Implemented")]
        public async Task ManagersGetByIdUnauthorized()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Not Implemented")]
        public async Task ManagersRegister()
        {
            var client = GameClientFactory.Create(this.Fixture.Server.CreateClient());

            throw new NotImplementedException();
        }

        [Fact(Skip = "Not Implemented")]
        public async Task ManagersRegisterAlreadyRegistered()
        {
            throw new NotImplementedException();
        }
    }
}
