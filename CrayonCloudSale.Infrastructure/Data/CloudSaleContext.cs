using CrayonCloudSale.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CrayonCloudSale.Infrastructure.Data;

public class CloudSaleContext : DbContext
{
    public CloudSaleContext(DbContextOptions<CloudSaleContext> options)
       : base(options)
    { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<PurchasedSoftware> PurchasedSoftwares { get; set; }
}

