using CrayonCloudSale.Infrastructure.Data.Models;

namespace CrayonCloudSale.Services.Interfaces;

public interface ICustomerService
{
    Task<Customer?> GetIncludingAccountsAsync(int customerId);
}
