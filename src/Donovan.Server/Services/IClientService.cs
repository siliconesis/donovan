using System.Threading.Tasks;
using Donovan.Server.Security;

namespace Donovan.Server.Services
{
    public interface IClientService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
    }
}
