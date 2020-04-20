using System;
using System.Threading.Tasks;
using Donovan.Game;

namespace Donovan.Server.Repositories
{
    public interface IManagerRepository
    {
        Task<Manager> CreateManagerAsync(Manager manager);

        Task<Manager> GetManagerAsync(string email);
    }
}
