using System.Threading.Tasks;
using Donovan.Game.Security;

namespace Donovan.Game.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetClientAsync(string id);
    }
}
