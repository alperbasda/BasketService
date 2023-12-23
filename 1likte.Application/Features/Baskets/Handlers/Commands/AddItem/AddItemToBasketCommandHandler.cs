using _1likte.Application.Contracts;
using _1likte.Application.Features.Baskets.Commands.AddItem;
using _1likte.Application.Features.Baskets.Rules;
using MediatR;

namespace _1likte.Application.Features.Baskets.Handlers.Commands.AddItem;

public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommand, AddItemToBasketResponse>
{
    IBasketDal _basketDal;
    BasketBusinessRules _basketBusinessRules;
    public AddItemToBasketCommandHandler(IBasketDal basketDal, BasketBusinessRules basketBusinessRules)
    {
        _basketDal = basketDal;
        _basketBusinessRules = basketBusinessRules;
    }

    public async Task<AddItemToBasketResponse> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await _basketBusinessRules.CreateBasketIfNotExistsForUser(request.UserId);
        var basketItem = await _basketBusinessRules.CreateBasketItemFromCommand(request);

        basket.Items.Add(basketItem);

        await _basketDal.UpdateAsync(basket);

        return new AddItemToBasketResponse
        {
            Amount = basketItem.Amount,
            ProductName = basketItem.ProductName,
            UnitPrice = basketItem.UnitPrice,
        };
    }
}
