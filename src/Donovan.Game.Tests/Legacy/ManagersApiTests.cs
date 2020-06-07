using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Donovan.Game.Tests.Core;
using Donovan.Game.Tests.Utilities;
using Donovan.Game.Security;
using Donovan.Utilities;

namespace Donovan.Game.Tests
{
    [Collection("TestEnvironment")]
    public class ManagersApiTests
    {
        private WebHostFixture Fixture { get; }

        public ManagersApiTests(WebHostFixture fixture)
        {
            this.Fixture = fixture;
        }

        // TODO: Use ClassData or MemberData to get environment-based test credentials.

        [Theory]
        [InlineData("donovan_tests", "QJIE3ZQeY1f10K146DA7IkpWy4bMXnUG", "fred@bedrock.com", "Fred Flintstone")]
        public async Task ManagersGet(string clientId, string clientSecret, string managerEmail, string managerName)
        {
            var clientToken = await this.GetClientTokenAsync(clientId, clientSecret);

            var managerToken = null as string;

            using (var client = this.Fixture.Server.CreateClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);

                using (var response = await client.GetAsync(new Uri(client.BaseAddress, $"{WebApiUris.Managers}/{Base64Helper.ToBase64(managerEmail)}")))
                {
                    response.EnsureSuccessStatusCode();

                    var manager = await response.Content.ReadAsAsync<Manager>();

                    Assert.Equal(managerName, manager.Name);
                }
            }
        }

        [Theory]
        [InlineData("fred@bedrock.com")]
        public async Task ManagersGetUnauthorized(string managerEmail)
        {
            using (var client = this.Fixture.Server.CreateClient())
            using (var response = await client.GetAsync(new Uri(client.BaseAddress, $"{WebApiUris.Managers}/{Base64Helper.ToBase64(managerEmail)}")))
            {
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Theory]
        [InlineData("donovan_tests", "QJIE3ZQeY1f10K146DA7IkpWy4bMXnUG", "barney@bedrock.com", "Barney Rubble", "VyxXyYXyV6es2azkRPWLD4kpYRjaMYyG")]
        public async Task ManagersRegister(string clientId, string clientSecret, string managerEmail, string managerName, string managerPassword)
        {
            var clientToken = await this.GetClientTokenAsync(clientId, clientSecret);

            using (var client = this.Fixture.Server.CreateClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);

                using (var content = RequestHelper.CreateRequestContentForManagerRegistration(managerEmail, managerName, managerPassword))
                using (var response = await client.PostAsync(new Uri(client.BaseAddress, WebApiUris.Managers), content))
                {
                    response.EnsureSuccessStatusCode();

                    var registration = await response.Content.ReadAsAsync<RegistrationResponse>();

                    var manager = registration.Manager;

                    Assert.Equal(managerName, manager.Name);
                }
            }
        }

        [Fact(Skip = "Not Implemented")]
        public async Task ManagersRegisterAlreadyRegistered()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Not Implemented")]
        public async Task ManagersRegisterUnauthorized()
        {
            throw new NotImplementedException();
        }

        private async Task<string> GetClientTokenAsync(string clientId, string clientSecret)
        {
            var clientToken = null as string;

            using (var client = this.Fixture.Server.CreateClient())
            using (var content = RequestHelper.CreateRequestContentForClientAuthentication(clientId, clientSecret))
            using (var response = await client.PostAsync(new Uri(client.BaseAddress, WebApiUris.ClientsAuthenticate), content))
            {
                response.EnsureSuccessStatusCode();

                var authenticationResponse = await response.Content.ReadAsAsync<AuthenticationResponse>();

                clientToken = authenticationResponse.Token;
            }

            return clientToken;
        }
    }
}
