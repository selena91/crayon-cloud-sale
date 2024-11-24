using CrayonCloudSale.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CrayonCloudSale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CcpController : ControllerBase
    {
        private readonly ILogger<CcpController> _logger;
        private readonly ICcpService _ccpService;

        public CcpController(ILogger<CcpController> logger, ICcpService ccpService)
        {
            _ccpService = ccpService;
            _logger = logger;
        }


        [HttpGet("GetSoftwareServices")]
        public async Task<IActionResult> GetSoftwareServices()
        {
            var services = await _ccpService.GetSoftwareServices();

            if (services.Any())
            {
                return Ok(JsonSerializer.Serialize(services));
            }
            _logger.LogError($"No services from ccp found");
            return BadRequest("No services found");
        }

        [HttpPost("OrderSoftwareServices")]
        public async Task<IActionResult> OrderSoftwareServices(int accountId, string serviceName, int quantity)
        {
            try { await _ccpService.OrderSoftware(accountId, serviceName, quantity); }
            catch (Exception ex)
            {
                _logger.LogError($"Ordering service for account with id {accountId} failed. Exception: {ex.Message}");
                return BadRequest(ex.Message);
            }

            return BadRequest("Service succefully ordered");
        }
    }
}
