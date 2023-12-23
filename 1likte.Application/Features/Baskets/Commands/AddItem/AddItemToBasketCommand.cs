using MediatR;

namespace _1likte.Application.Features.Baskets.Commands.AddItem;

public class AddItemToBasketCommand : IRequest<AddItemToBasketResponse>
{
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public Guid MerchantId { get; set; }

    public int Amount { get; set; }
}
