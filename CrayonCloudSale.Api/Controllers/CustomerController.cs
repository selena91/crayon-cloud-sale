using CrayonCloudSale.Core.DTOs;
using CrayonCloudSale.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CrayonCloudSale.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet(Name = "getAccountByCustomer")]
        public async Task<IActionResult> GetAccounts(int customerId)
        {
            var cutomer = await _customerService.GetIncludingAccountsAsync(customerId);

            if(cutomer != null)
            {
                var responseObj = cutomer.Accounts.Select(p => new AccountDTO(p.Name, p.CreateDate));
                return Ok(JsonSerializer.Serialize(responseObj));
            }

            _logger.LogError($"No customer with id {customerId} found");
            return BadRequest("No customer found");
        }
    }
}
