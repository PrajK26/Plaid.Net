using Acklann.Plaid.Management;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Acklann.Plaid.Demo.DataLayer
{
    public class TokenService : ITokenService
    {
        private readonly DBContext _dbContext;

        public TokenService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Entity> GetItemAsync(string institutionId)
        {
            var existingRecord = await _dbContext.ExchangeTokenResponse
                .FirstOrDefaultAsync(e =>
                    e.InstitutionId == institutionId);

            return existingRecord;
        }

        public async Task<bool> SaveTokenAsync(Entity entity)
        {
            try
            {
                _dbContext.ExchangeTokenResponse.Add(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
