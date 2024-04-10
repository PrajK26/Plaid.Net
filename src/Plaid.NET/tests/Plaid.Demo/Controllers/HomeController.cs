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
            ExchangeTokenResponse result;
            var client = new PlaidClient(environment);
            var item = await _tokenService.GetItemAsync(metadata.Institution.Id);

            if (item == null)
            {
                result = client.ExchangeTokenAsync(new ExchangeTokenRequest()
                {
                    Secret = _credentials.Secret,
                    ClientId = _credentials.ClientId,
                    PublicToken = metadata.PublicToken
                }).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var entity = new DataLayer.Entity
                    {
                        ItemId = result.ItemId,
                        RequestId = result.RequestId,
                        InstitutionId = metadata.Institution.Id,
                        AccessToken = result.AccessToken
                    };

                    await _tokenService.SaveTokenAsync(entity);
                }

                _credentials.AccessToken = result.AccessToken;
                System.Diagnostics.Debug.WriteLine($"access_token: '{result.AccessToken}'");
            }
            else
            {
                result = new ExchangeTokenResponse
                {
                    ItemId = item.ItemId,
                    RequestId = item.RequestId,
                    AccessToken = item.AccessToken
                };
            }

            return Ok(result);
        }

        #region Private Members

        private Middleware.PlaidCredentials _credentials;

        #endregion Private Members
    }
}