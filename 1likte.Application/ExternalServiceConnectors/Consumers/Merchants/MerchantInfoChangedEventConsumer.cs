using _1likte.Application.Features.Baskets.Commands.UpdateMerchantInfo;
using _1likte.Events.Merchants;
using AutoMapper;
using MassTransit;
using MediatR;

namespace _1likte.Application.ExternalServiceConnectors.Consumers.Merchants;

public class MerchantInfoChangedEventConsumer : IConsumer<MerchantInfoChangedEvent>
{
    IMediator _mediator;
    IMapper _mapper;
    public MerchantInfoChangedEventConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<MerchantInfoChangedEvent> context)
    {
        await _mediator.Send(_mapper.Map<UpdateMerchantInfoNotification>( context.Message));
    }
}
