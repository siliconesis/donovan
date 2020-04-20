using System.Net.Http;

namespace Donovan.Client
{
    /// <summary>
    /// Creates instances of the game client.
    /// </summary>
    /// <remarks>
    /// This allows Donovan project developers to create game clients using
    /// HTTP client instances from the in-memory TestServer class.
    /// </remarks>
    public static class GameClientFactory
    {
        public static GameClient Create(HttpClient client)
        {
            return new GameClient(client);
        }
    }
}
