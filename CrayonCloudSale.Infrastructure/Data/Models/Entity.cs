namespace CrayonCloudSale.Infrastructure.Data.Models;

public abstract class Entity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? ChangeDate { get; set; }
}

