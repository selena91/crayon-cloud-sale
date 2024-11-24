using CrayonCloudSale.Infrastructure.Data.Models;
using CrayonCloudSale.Infrastructure.UnitOfWork;
using CrayonCloudSale.Services.Interfaces;

namespace CrayonCloudSale.Infrastructure.Services;

public class PurchasedSoftwareService : IPurchasedSoftwareService
{
    private readonly IUnitOfWork _unitOfWork;

    public PurchasedSoftwareService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

    public async Task CancelPurchase(long id)
    {
        var purchase = await _unitOfWork.PurchasedSoftwareRepository.GetByIdAsync(id);
        if (purchase != null)
        {
            purchase.State = State.Canceled;
            purchase.ChangeDate = DateTime.UtcNow;
            _unitOfWork.PurchasedSoftwareRepository.Update(purchase);
        }
        else
        {
            throw new InvalidOperationException($"Software purchase with id {id} not found.");
        }
    }

    public async Task ChangeQuantity(long id, int quantity)
    {
        var purchase = await _unitOfWork.PurchasedSoftwareRepository.GetByIdAsync(id);
        if (purchase != null)
        {
            purchase.Quantity = quantity;
            purchase.ChangeDate = DateTime.UtcNow;
            _unitOfWork.PurchasedSoftwareRepository.Update(purchase);
        }
        else
        {
            throw new InvalidOperationException($"Software purchase with id {id} not found.");
        }
    }

    public async Task ExtendExpiryDate(long id, DateTime validTo)
    {
        var purchase = await _unitOfWork.PurchasedSoftwareRepository.GetByIdAsync(id);

        if (purchase != null)
        {
            if (purchase.ValidTo > validTo)
            {
                throw new InvalidOperationException($"Expiry date has to be greater then {purchase.ValidTo}");
            }

            purchase.ValidTo = validTo;
            purchase.ChangeDate = DateTime.UtcNow;
            _unitOfWork.PurchasedSoftwareRepository.Update(purchase);
        }
        else
        {
            throw new InvalidOperationException($"Software purchase with id {id} not found.");
        }
    }
}

