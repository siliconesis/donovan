using System.Threading.Tasks;
using Donovan.Game.Repositories;
using Donovan.Game.Security;

namespace Donovan.Game.Services
{
    public class ClientService : IClientService
    {
        private readonly IConfigurationRepository configurationRepository;
        private readonly IClientRepository clientRepository;

        private string TokenSigningKey { get; }

        public ClientService(IConfigurationRepository configurationRepository, IClientRepository clientRepository)
        {
            this.configurationRepository = configurationRepository;
            this.clientRepository = clientRepository;

            this.TokenSigningKey = this.configurationRepository.GetSettingAsync("Token", "SigningKey")
                .Result
                .Value;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var response = null as AuthenticationResponse;

            var client = await this.clientRepository.GetClientAsync(request.Id)
                .ConfigureAwait(false);

            if (client != null)
            {
                if ((client.IsEnabled) && (PasswordHelper.IsValid(request.Secret, client.Secret)))
                {
                    var token = TokenHelper.CreateToken(
                        this.TokenSigningKey,
                        client.Id,
                        client.Name);

                    response = new AuthenticationResponse()
                    {
                        Token = TokenHelper.EncodeToken(token)
                    };
                }
            }

            return response;
        }
    }
}
