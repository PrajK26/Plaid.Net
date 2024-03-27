using Acklann.Plaid.Management;
using System.Threading.Tasks;

namespace Acklann.Plaid.Demo.DataLayer
{
    public interface ITokenService
    {
        Task<bool> SaveExchangeTokenResponseIfNotExistsAsync(Entity entity);
    }
}
