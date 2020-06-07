using System.Net.Http;
using System.Text;

namespace Donovan.Game.Tests.Utilities
{
    public static class RequestHelper
    {
        /// <summary>
        /// Creates content for a client authentication request.
        /// </summary>
        public static HttpContent CreateRequestContentForClientAuthentication(string id, string secret)
        {
            var json = $"{{ \"id\": \"{id}\", \"secret\": \"{secret}\" }}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        /// <summary>
        /// Creates content for a manager registration request.
        /// </summary>
        public static HttpContent CreateRequestContentForManagerRegistration(string email, string name, string password)
        {
            var json = $"{{ \"email\": \"{email}\", \"name\": \"{name}\", \"password\": \"{password}\" }}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        /// <summary>
        /// Creates content for a manager sign-in request.
        /// </summary>
        public static HttpContent CreateRequestContentForManagerSignin(string email, string password)
        {
            var json = $"{{ \"email\": \"{email}\", \"password\": \"{password}\" }}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }
    }
}
