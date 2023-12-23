using _1likte.Application.Features.Baskets.Commands.UpdateProductInfo;
using _1likte.Events.Products;
using AutoMapper;
using MassTransit;
using MediatR;

namespace _1likte.Application.ExternalServiceConnectors.Consumers.Products;

public class ProductInfoChangedEventConsumer : IConsumer<ProductInfoChangedEvent>
{
    IMediator _mediator;
    IMapper _mapper;
    public ProductInfoChangedEventConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<ProductInfoChangedEvent> context)
    {
        await _mediator.Send(_mapper.Map<UpdateProductInfoNotification>(context.Message));
    }
}
