namespace CrayonCloudSale.Infrastructure.Data.Models;

public class PurchasedSoftware : Entity
{
    public int Quantity { get; set; }
    public State State { get; set; }
    public DateTime ValidTo { get; set; }
    public Account Account { get; set; }
}

