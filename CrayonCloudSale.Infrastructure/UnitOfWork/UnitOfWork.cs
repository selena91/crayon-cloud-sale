using CrayonCloudSale.Core.BaseUoW;
using CrayonCloudSale.Infrastructure.Data;
using CrayonCloudSale.Infrastructure.Data.Models;
using CrayonCloudSale.Infrastructure.GenericRepository;

namespace CrayonCloudSale.Infrastructure.UnitOfWork;

public class UnitOfWork: BaseUoW<CloudSaleContext>, IUnitOfWork
{
    public IRepository<Account> AccountRepository { get; }
    public IRepository<Customer> CustomerRepository { get; }
    public IRepository<PurchasedSoftware> PurchasedSoftwareRepository { get; }

    public UnitOfWork(CloudSaleContext dbContext, IRepository<Account> accountRepository, IRepository<Customer> customerRepository, IRepository<PurchasedSoftware> purchasedSoftwareRepository):base(dbContext)
    {
        AccountRepository = accountRepository;
        CustomerRepository = customerRepository;
        PurchasedSoftwareRepository = purchasedSoftwareRepository;
    }
}