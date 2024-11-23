using CrayonCloudSale.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrayonCloudSale.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchasedSoftwareController : ControllerBase
    {
        private readonly ILogger<PurchasedSoftwareController> _logger;
        private readonly IPurchasedSoftwareService _purchasedSoftwareService;

        public PurchasedSoftwareController(ILogger<PurchasedSoftwareController> logger, IPurchasedSoftwareService purchasedSoftwareService)
        {
            _logger = logger;
            _purchasedSoftwareService = purchasedSoftwareService;
        }

        [HttpDelete("cancelPurchase")]
        public async Task<IActionResult> CancelPurchases(int purchaseId)
        {
            try { await _purchasedSoftwareService.CancelPurchase(purchaseId); }
            catch (Exception ex)
            {
                _logger.LogError($"Canceling purchase with id {purchaseId} failed. Exception: {ex.Message}");
                return BadRequest(ex.Message);
            }

            return Ok("Purchase canceled");
        }

        [HttpPut("changeQuantity")]
        public async Task<IActionResult> ChangeQuantity(int purchaseId, int quantity)
        {
            try { await _purchasedSoftwareService.ChangeQuantity(purchaseId, quantity); }
            catch (Exception ex)
            {
                _logger.LogError($"Changing quantity for purchase with id {purchaseId} failed. Exception: {ex.Message}");
                return BadRequest(ex.Message);
            }

            return Ok("Quantity changed successfully");
        }

        [HttpPut("extendLicense")]
        public async Task<IActionResult> ExtendLicense(int purchaseId, DateTime validTo)
        {
            try { await _purchasedSoftwareService.ExtendExpiryDate(purchaseId, validTo); }
            catch (Exception ex)
            {
                _logger.LogError($"Extending license for purchase with id {purchaseId} failed. Exception: {ex.Message}");
                return BadRequest(ex.Message);
            }

            return Ok("License extended successfully");
        }
    }
}
