using CrayonCloudSale.Infrastructure.Data.Models;
using CrayonCloudSale.Infrastructure.UnitOfWork;
using CrayonCloudSale.Services.Interfaces;

namespace CrayonCloudSale.Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

    public async Task<Customer?> GetIncludingAccountsAsync(int customerId)
    {
        return (await _unitOfWork.CustomerRepository.GetAsyncWithoutTracking(c=>c.Id==customerId,null,c=>c.Accounts)).FirstOrDefault();
    }
}
