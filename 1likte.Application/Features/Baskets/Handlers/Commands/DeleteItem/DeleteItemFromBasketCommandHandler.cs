using _1likte.Application.Contracts;
using _1likte.Application.Features.Baskets.Commands.DeleteItem;
using _1likte.Application.Features.Baskets.Rules;
using MediatR;

namespace _1likte.Application.Features.Baskets.Handlers.Commands.DeleteItem;

public class DeleteItemFromBasketCommandHandler : IRequestHandler<DeleteItemFromBasketCommand, DeleteItemFromBasketResponse>
{
    IBasketDal _basketDal;
    BasketBusinessRules _basketBusinessRules;
    public DeleteItemFromBasketCommandHandler(IBasketDal basketDal, BasketBusinessRules basketBusinessRules)
    {
        _basketDal = basketDal;
        _basketBusinessRules = basketBusinessRules;
    }

    public async Task<DeleteItemFromBasketResponse> Handle(DeleteItemFromBasketCommand request, CancellationToken cancellationToken)
    {
        var data = await _basketDal.GetAsync(q => q.UserId == request.UserId);

        await _basketBusinessRules.ThrowExceptionIfDataNull(data);

        data!.Items.RemoveAll(w => w.ProductId == request.ProductId && w.MerchantId == request.MerchantId);

        await _basketDal.UpdateAsync(data);

        return new DeleteItemFromBasketResponse
        {
            MerchantId = request.MerchantId,
            ProductId = request.ProductId,
        };
    }
}
