using System;
using System.Threading;
using System.Threading.Tasks;
using Donovan.Models;

namespace Donovan.Server.Repositories
{
    public interface IManagerRepository
    {
        Task<Manager> GetAsync(string id);
    }
}
