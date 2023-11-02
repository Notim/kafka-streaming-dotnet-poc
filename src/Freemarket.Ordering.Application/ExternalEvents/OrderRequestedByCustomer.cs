namespace Freemarket.Ordering.Applications.ExternalEvents;

public class OrderRequestedByCustomer
{

    public Guid? OrderId { get; set; }

    public decimal? TotalValue { get; set; }

    public string? ClientName { get; set; }

}