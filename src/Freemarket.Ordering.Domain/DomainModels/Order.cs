namespace Freemarket.Ordering.Domain.DomainModels;

public class Order
{

    public Guid? OrderId { get; set; }

    public decimal? TotalValue { get; set; }

    public string? ClientName { get; set; }

}