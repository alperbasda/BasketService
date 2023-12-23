namespace _1likte.Application.Features.Baskets.Queries.GetByUserId;

public class GetByUserIdBasketResponse
{
    public Guid UserId { get; set; }

    public List<GetByUserIdBasketItemResponse> Items { get; set; }

    public decimal TotalPrice => Items.Sum(q => q.UnitPrice * q.Amount);
}

public class GetByUserIdBasketItemResponse
{
    public Guid ProductId { get; set; }

    public Guid MerchantId { get; set; }

    public string MerchantName { get; set; }

    public string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public int Amount { get; set; }
}
