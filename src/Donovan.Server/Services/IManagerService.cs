using System;
using System.Threading;
using System.Threading.Tasks;
using Donovan;
using Donovan.Models;

namespace Donovan.Server.Services
{
    public interface IManagerService
    {
        Task<Manager> GetAsync(string id);
    }
}
