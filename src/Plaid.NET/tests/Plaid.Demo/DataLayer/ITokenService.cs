using Acklann.Plaid.Management;
using System.Threading.Tasks;

namespace Acklann.Plaid.Demo.DataLayer
{
    public interface ITokenService
    {
        Task<Entity> GetItemAsync(string institutionId);

        Task<bool> SaveTokenAsync(Entity entity);
    }
}
