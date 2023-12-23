using _1likte.Application.Features.Baskets.Queries.GetByUserId;
using _1likte.Application.Features.Baskets.Rules;
using AutoMapper;
using MediatR;

namespace _1likte.Application.Features.Baskets.Handlers.Queries.GetByUserId;

public class GetByUserIdBasketQueryHandler : IRequestHandler<GetByUserIdBasketQuery, GetByUserIdBasketResponse>
{
    BasketBusinessRules _basketBusinessRules;
    IMapper _mapper;

    public GetByUserIdBasketQueryHandler(BasketBusinessRules basketBusinessRules, IMapper mapper)
    {
        _basketBusinessRules = basketBusinessRules;
        _mapper = mapper;
    }

    public async Task<GetByUserIdBasketResponse> Handle(GetByUserIdBasketQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<GetByUserIdBasketResponse>(await _basketBusinessRules.CreateBasketIfNotExistsForUser(request.UserId));

    }
}
