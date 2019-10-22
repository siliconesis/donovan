using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Donovan;
using Donovan.Models;
using Donovan.Utilities;

namespace Donovan.Client
{
    public class GameClient
    {
        private readonly UriBuilder builder = new UriBuilder("http://localhost/");
        private readonly HttpClient client;

        /// <summary>
        /// Creates an instance of the game client.
        /// </summary>
        public GameClient() : this(new HttpClient())
        {
        }

        /// <summary>
        /// Creates an instance of the game client.
        /// </summary>
        /// <remarks>
        /// Clients should use the default constructor. This overload is provided to support
        /// development. For example, use this overload to provide the client created by the
        /// in-memory test server (Microsoft.AspNetCore.TestHost.TestServer).
        /// </remarks>
        internal GameClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Manager> GetManagerAsync(string email)
        {
            var id = Encoder.ToBase64(email);

            this.builder.Path = $"/api/managers/{id}";

            // TODO: Exception handling; do not pass raw API errors to client.
            using (var response = await this.client.GetAsync(this.builder.Uri))
            {
                response.EnsureSuccessStatusCode();

                var manager = await response.Content.ReadAsAsync<Manager>();

                return manager;
            }
        }

        public async Task<Manager> RegisterManagerAsync(string email, string provider)
        {
            return await Task.Run(() => { return new Manager(); });
        }
    }

    /*
    public IEnumerable<Manager> GetManagers(string searchText)
    {
        return new List<Manager>();
    }
    */
}
