using Acklann.Plaid.Demo.DataLayer;
using Acklann.Plaid.Management;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Acklann.Plaid.Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITokenService _tokenService;

        public HomeController(Middleware.PlaidCredentials credentials, 
            ITokenService tokenService)
        {
            _credentials = credentials;
            _tokenService = tokenService;
        }

        public IActionResult Index()
        {
            return View(_credentials);
        }

        [HttpPost]
        public async Task<IActionResult> GetAccessToken(Environment environment, [FromBody]PlaidLinkResponse metadata)
        {
            var client = new PlaidClient(environment);
            ExchangeTokenResponse result = client.ExchangeTokenAsync(new ExchangeTokenRequest()
            {
                Secret = _credentials.Secret,
                ClientId = _credentials.ClientId,
                PublicToken = metadata.PublicToken
            }).Result;

            var entity = new DataLayer.Entity
            {
                ItemId = result.ItemId,
                RequestId = result.RequestId,
                AccessToken = result.AccessToken,
                StatusCode = result.StatusCode
            };

            await _tokenService.SaveExchangeTokenResponseIfNotExistsAsync(entity);

            _credentials.AccessToken = result.AccessToken;
            System.Diagnostics.Debug.WriteLine($"access_token: '{result.AccessToken}'");

            return Ok(result);
        }

        #region Private Members

        private Middleware.PlaidCredentials _credentials;

        #endregion Private Members
    }
}