using CrayonCloudSale.Infrastructure.Data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CrayonCloudSale.Infrastructure.Data;

public static class SeedDataHelper
{
    public static void SeedData(IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<CloudSaleContext>();
            var customers = new List<Customer>
        {
            new Customer {Name= "Createq", CreateDate = DateTime.Now, ChangeDate= DateTime.Now},
            new Customer {Name= "Test customer", CreateDate = DateTime.Now, ChangeDate= DateTime.Now},
        };

            var accounts = new List<Account>
        {
            new Account {Name= "Test account 1", Customer = customers[0], CreateDate = DateTime.Now, ChangeDate= DateTime.Now},
            new Account {Name= "Test account 2", Customer = customers[0], CreateDate = DateTime.Now, ChangeDate= DateTime.Now},
            new Account {Name= "Test account 3", Customer = customers[1], CreateDate = DateTime.Now, ChangeDate= DateTime.Now},
        };

            var purchasedSoftwares = new List<PurchasedSoftware>
        {
            new PurchasedSoftware {Name="Microsoft Office 365", Quantity = 5, Account = accounts[0], State=State.Active, CreateDate = DateTime.Now, ChangeDate= DateTime.Now},
            new PurchasedSoftware {Name="Adobe Creative Cloud", Quantity = 5, Account = accounts[0],State=State.Active, CreateDate = DateTime.Now, ChangeDate= DateTime.Now},
            new PurchasedSoftware {Name="Autodesk AutoCAD", Quantity = 5, Account = accounts[0], State=State.Active,CreateDate = DateTime.Now, ChangeDate= DateTime.Now},
            new PurchasedSoftware {Name="Zoom", Quantity = 5, Account = accounts[0], State=State.Active,CreateDate = DateTime.Now, ChangeDate= DateTime.Now},
            new PurchasedSoftware {Name="QuickBooks", Quantity = 5, Account = accounts[1], State=State.Active,CreateDate = DateTime.Now, ChangeDate= DateTime.Now},
        };

            if (!context.Customers.Any())
            {
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            if (!context.Accounts.Any())
            {
                context.Accounts.AddRange(accounts);
                context.SaveChanges();
            }

            if (!context.PurchasedSoftwares.Any())
            {
                context.PurchasedSoftwares.AddRange(purchasedSoftwares);
                context.SaveChanges();
            }
        }
    }
}
