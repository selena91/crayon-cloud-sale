namespace CrayonCloudSale.Services.Interfaces;

public interface IPurchasedSoftwareService
{
    Task CancelPurchase(long id);
    Task ChangeQuantity(long id, int quantity);
    Task ExtendExpiryDate(long id, DateTime validTo);
}
