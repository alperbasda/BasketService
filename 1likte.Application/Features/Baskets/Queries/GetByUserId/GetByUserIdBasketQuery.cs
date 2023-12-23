using MediatR;

namespace _1likte.Application.Features.Baskets.Queries.GetByUserId;

public class GetByUserIdBasketQuery : IRequest<GetByUserIdBasketResponse> /*, ICachableRequest*/
{
    public Guid UserId { get; set; }

    //Cachelenebilir ama şuan konumuz bu değil.

    //public string CacheKey => throw new NotImplementedException();

    //public bool BypassCache => throw new NotImplementedException();

    //public string? CacheGroupKey => throw new NotImplementedException();

    //public TimeSpan? SlidingExpiration => throw new NotImplementedException();
}
