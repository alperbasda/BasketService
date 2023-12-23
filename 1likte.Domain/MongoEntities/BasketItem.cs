namespace _1likte.Domain.MongoEntities;

public class BasketItem
{
    public Guid ProductId { get; set; }

    public Guid MerchantId { get; set; }

    public string MerchantName { get; set; }

    public string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public int Amount { get; set; }
}
