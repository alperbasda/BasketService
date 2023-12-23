using _1likte.Application.Features.Baskets.Commands.UpdateMerchantInfo;
using _1likte.Application.Features.Baskets.Commands.UpdateProductInfo;
using _1likte.Application.Features.Baskets.Queries.GetByUserId;
using _1likte.Domain.MongoEntities;
using _1likte.Events.Merchants;
using _1likte.Events.Products;
using AutoMapper;

namespace _1likte.Application.Features.Baskets.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Basket, GetByUserIdBasketResponse>();
        CreateMap<BasketItem, GetByUserIdBasketItemResponse>();

        CreateMap<MerchantInfoChangedEvent, UpdateMerchantInfoNotification>();
        CreateMap<ProductInfoChangedEvent, UpdateProductInfoNotification>();
    }
}
