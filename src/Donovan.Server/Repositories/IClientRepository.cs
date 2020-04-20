using System.Threading.Tasks;
using Donovan.Server.Security;

namespace Donovan.Server.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetClientAsync(string id);
    }
}
