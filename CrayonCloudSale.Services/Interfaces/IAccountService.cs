using CrayonCloudSale.Infrastructure.Data.Models;

namespace CrayonCloudSale.Services.Interfaces;

public interface IAccountService
{
    Task<Account?> GetWithPurchasedSoftwares(int accountId);
}

