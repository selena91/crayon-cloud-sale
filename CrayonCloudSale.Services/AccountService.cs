using CrayonCloudSale.Infrastructure.Data.Models;
using CrayonCloudSale.Infrastructure.UnitOfWork;
using CrayonCloudSale.Services.Interfaces;

namespace CrayonCloudSale.Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IUnitOfWork unitOfWork) {  _unitOfWork = unitOfWork; }

    public async Task<Account?> GetWithPurchasedSoftwares(int accountId)
    {
        return (await _unitOfWork.AccountRepository.GetAsyncWithoutTracking(a=>a.Id == accountId, null, a=>a.PurchasedSoftwares)).FirstOrDefault();
    }
}

