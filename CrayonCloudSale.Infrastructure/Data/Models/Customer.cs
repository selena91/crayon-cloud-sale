namespace CrayonCloudSale.Infrastructure.Data.Models;

public class Customer : Entity
{
    public ICollection<Account>? Accounts { get; set; }
}

