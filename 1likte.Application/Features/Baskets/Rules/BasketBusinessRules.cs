using _1likte.Application.Base;
using _1likte.Application.Contracts;
using _1likte.Application.ExternalServiceConnectors.HttpClients.Merchants;
using _1likte.Application.ExternalServiceConnectors.HttpClients.Products;
using _1likte.Application.Features.Baskets.Commands.AddItem;
using _1likte.Domain.ApiModels.Merchants;
using _1likte.Domain.ApiModels.Products;
using _1likte.Domain.MongoEntities;

namespace _1likte.Application.Features.Baskets.Rules;

public class BasketBusinessRules : BaseBusinessRules
{
    IBasketDal _basketDal;
    IMerchantClient _merchantClient;
    IProductClient _productClient;

    public BasketBusinessRules(IBasketDal basketDal, IMerchantClient merchantClient, IProductClient productClient)
    {
        _basketDal = basketDal;
        _merchantClient = merchantClient;
        _productClient = productClient;
    }

    public async Task<Basket> CreateBasketIfNotExistsForUser(Guid userId)
    {
        var basket = await _basketDal.GetAsync(w => w.UserId == userId);
        if (basket != null)
            return basket;

        basket = new Basket
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Items = new List<BasketItem>(),
            CreatedTime = DateTime.UtcNow,
            DeletedTime = null,
            UpdatedTime = null
        };
        await _basketDal.AddAsync(basket);

        return basket;

    }

    public async Task<BasketItem> CreateBasketItemFromCommand(AddItemToBasketCommand command)
    {
        var data = new BasketItem
        {
            Amount = command.Amount,
            MerchantId = command.MerchantId,
            ProductId = command.ProductId,
        };
        var product = await GetProductInfo(data.ProductId);
        data.ProductName = product.Name;
        data.UnitPrice = product.UnitPrice;

        var merchant = await GetMerchantInfo(data.MerchantId);   
        data.MerchantName = merchant.Name;

        return data;
    }

    private async Task<MerchantInfoResponse> GetMerchantInfo(Guid id)
    {
        var data = await _merchantClient.GetMerchantInfo(id);
        await base.ThrowExceptionIfDataNull(data);
        return data;
    }

    private async Task<ProductInfoResponse> GetProductInfo(Guid id)
    {
        var data =  await _productClient.GetProductInfo(id);
        await base.ThrowExceptionIfDataNull(data);
        return data;
    }
}
