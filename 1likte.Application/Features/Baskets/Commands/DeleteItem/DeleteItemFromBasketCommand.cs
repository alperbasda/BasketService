using MediatR;

namespace _1likte.Application.Features.Baskets.Commands.DeleteItem;

public class DeleteItemFromBasketCommand : IRequest<DeleteItemFromBasketResponse>
{
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public Guid MerchantId { get; set; }
}
