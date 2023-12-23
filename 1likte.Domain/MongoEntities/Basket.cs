using Core.Persistence.Models;

namespace _1likte.Domain.MongoEntities;

public class Basket : MongoEntity<Guid>
{
    public Basket()
    {
        Items = new List<BasketItem>();
    }

    public Guid UserId { get; set; }

    public List<BasketItem> Items { get; set; }

    public decimal TotalPrice => Items.Sum(q => q.UnitPrice * q.Amount);
}

