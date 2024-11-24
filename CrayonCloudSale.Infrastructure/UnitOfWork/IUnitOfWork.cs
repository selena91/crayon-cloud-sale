using CrayonCloudSale.Core.BaseUoW;
using CrayonCloudSale.Infrastructure.Data.Models;
using CrayonCloudSale.Infrastructure.GenericRepository;

namespace CrayonCloudSale.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IBaseUoW
{
    IRepository<Account> AccountRepository { get; }
    IRepository<Customer> CustomerRepository { get; }
    IRepository<PurchasedSoftware> PurchasedSoftwareRepository { get; }
}