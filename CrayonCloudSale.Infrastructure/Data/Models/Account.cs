namespace CrayonCloudSale.Infrastructure.Data.Models;

public class Account : Entity
{
    public Customer Customer { get; set; }

    public ICollection<PurchasedSoftware> PurchasedSoftwares { get; set; }
}

