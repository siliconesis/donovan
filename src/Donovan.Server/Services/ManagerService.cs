using System;
using System.Threading;
using System.Threading.Tasks;
using Donovan;
using Donovan.Models;
using Donovan.Server;
using Donovan.Server.Repositories;

namespace Donovan.Server.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository repository;

        public ManagerService(IManagerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Manager> GetAsync(string id)
        {
            return await this.repository.GetAsync(id);
        }
    }
}
