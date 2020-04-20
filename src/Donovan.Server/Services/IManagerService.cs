using System;
using System.Threading.Tasks;
using Donovan.Game;

namespace Donovan.Server.Services
{
    public interface IManagerService
    {
        Task<Manager> GetAsync(string email);

        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);

        Task<SigninResponse> SignInAsync(SigninRequest request);
    }
}
