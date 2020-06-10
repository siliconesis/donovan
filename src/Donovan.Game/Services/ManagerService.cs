using System;
using System.Threading.Tasks;
using Donovan.Game;
using Donovan.Game.Repositories;
using Donovan.Game.Security;

namespace Donovan.Game.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IConfigurationRepository configurationRepository;
        private readonly IManagerRepository managerRepository;

        private string TokenSigningKey { get; }

        public ManagerService(IConfigurationRepository configurationRepository, IManagerRepository managerRepository)
        {
            this.configurationRepository = configurationRepository ??
                throw new ArgumentNullException(nameof(configurationRepository));

            this.managerRepository = managerRepository ??
                throw new ArgumentNullException(nameof(managerRepository));

            this.TokenSigningKey = this.configurationRepository.GetSettingAsync("Token", "SigningKey")
                .Result
                .Value;
        }

        public async Task<Manager> GetAsync(string email)
        {
            var manager = await this.managerRepository.GetManagerAsync(email)
                .ConfigureAwait(false);

            return manager;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var manager = new Manager()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Password = PasswordHelper.HashPassword(request.Password),
                RegisteredOn = DateTime.UtcNow,
                VerificationStatus = VerificationStatus.Pending,
                VerificationCode = Guid.NewGuid(),

                // TODO: Extract the verification expiration period to configuration.
                VerificationCodeExpiresOn = DateTime.UtcNow.AddHours(1)
            };

            manager = await this.managerRepository.CreateManagerAsync(manager)
                .ConfigureAwait(false);

            var response = new RegistrationResponse()
            {
                Manager = manager
            };

            return response;
        }

        public async Task<SigninResponse> SignInAsync(SigninRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = null as SigninResponse;

            var manager = await this.managerRepository.GetManagerAsync(request.Email)
                .ConfigureAwait(false);

            if (manager != null)
            {
                if ((manager.VerificationStatus == VerificationStatus.Verified) && (PasswordHelper.IsValid(request.Password, manager.Password)))
                {
                    var token = TokenHelper.CreateToken(
                        this.TokenSigningKey,
                        manager.Id.ToString(),
                        manager.Name);

                    response = new SigninResponse()
                    {
                        Token = TokenHelper.EncodeToken(token)
                    };
                }
            }

            return response;
        }
    }
}
