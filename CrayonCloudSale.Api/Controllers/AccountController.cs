using CrayonCloudSale.Core.DTOs;
using CrayonCloudSale.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CrayonCloudSale.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet("GetAccountPurchases")]
        public async Task<IActionResult> GetPurchases(int accountId)
        {
            var account = await _accountService.GetWithPurchasedSoftwares(accountId);

            if(account != null)
            {
                var responseObj = account.PurchasedSoftwares.Select(p => new PurchasedSoftwareDTO(p.Name, p.ValidTo, p.Quantity));
                return Ok(JsonSerializer.Serialize(responseObj));
            }

            _logger.LogError($"No account with id {accountId} found");
            return BadRequest("No account found");
        }
    }
}
