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

        public async Task<bool> SaveExchangeTokenResponseIfNotExistsAsync(Entity entity)
        {
            var existingRecord = await _dbContext.ExchangeTokenResponse
                .FirstOrDefaultAsync(e =>
                    e.ItemId == entity.ItemId &&
                    e.AccessToken == entity.AccessToken &&
                    e.StatusCode == entity.StatusCode);

            if (existingRecord == null)
            {
                _dbContext.ExchangeTokenResponse.Add(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
