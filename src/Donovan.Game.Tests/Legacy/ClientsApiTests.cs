using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Donovan.Game.Tests.Core;
using Donovan.Game.Tests.Utilities;
using Donovan.Game.Security;

namespace Donovan.Game.Tests
{
    [Collection("TestEnvironment")]
    public class ClientsApiTests
    {
        private WebHostFixture Fixture { get; }

        public ClientsApiTests(WebHostFixture fixture)
        {
            this.Fixture = fixture;
        }

        // TODO: Use ClassData or MemberData to get environment-based test credentials.

        [Theory]
        [InlineData("donovan_tests", "QJIE3ZQeY1f10K146DA7IkpWy4bMXnUG")]
        public async Task Authenticate(string id, string secret)
        {
            var encoded = null as string;

            using (var client = this.Fixture.Server.CreateClient())
            using (var content = RequestHelper.CreateRequestContentForClientAuthentication(id, secret))
            using (var response = await client.PostAsync(new Uri(client.BaseAddress, WebApiUris.ClientsAuthenticate), content))
            {
                var authenticationResponse = await response.Content.ReadAsAsync<AuthenticationResponse>();

                encoded = authenticationResponse.Token;
            }

            // TODO: Validate expected claims.
            Assert.NotNull(encoded);
        }

        [Theory]
        [InlineData("donovan_tests_disabled", "2VmqKpGmVpcnss54LWSuwaaS4skLv52b")]
        public async Task AuthenticateDisabled(string id, string secret)
        {
            using (var client = this.Fixture.Server.CreateClient())
            using (var content = RequestHelper.CreateRequestContentForClientAuthentication(id, secret))
            using (var response = await client.PostAsync(new Uri(client.BaseAddress, WebApiUris.ClientsAuthenticate), content))
            {
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Theory]
        [InlineData("donovan_tests_doesnotexist", "Sj6cgvn7DsrX8h3FYK4c57JxPMu94mxt")]
        public async Task AuthenticateDoesNotExist(string id, string secret)
        {
            using (var client = this.Fixture.Server.CreateClient())
            using (var content = RequestHelper.CreateRequestContentForClientAuthentication(id, secret))
            using (var response = await client.PostAsync(new Uri(client.BaseAddress, WebApiUris.ClientsAuthenticate), content))
            {
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Theory]
        [InlineData("donovan_tests", "dzJrXnhM8zhKKLbBfvmVjRpQM2zgbtu2")]
        public async Task AuthenticateSecretInvalid(string id, string secret)
        {
            using (var client = this.Fixture.Server.CreateClient())
            using (var content = RequestHelper.CreateRequestContentForClientAuthentication(id, secret))
            using (var response = await client.PostAsync(new Uri(client.BaseAddress, WebApiUris.ClientsAuthenticate), content))
            {
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
    }
}
