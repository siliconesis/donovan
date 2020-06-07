using System.Threading.Tasks;
using Donovan.Game.Security;

namespace Donovan.Game.Services
{
    public interface IClientService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
    }
}
