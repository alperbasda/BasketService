using _1likte.Application.Contracts;
using _1likte.Application.Features.Baskets.Commands.UpdateProductInfo;
using MediatR;

namespace _1likte.Application.Features.Baskets.Handlers.Commands.UpdateProductInfo;

public class UpdateProductInfoNotificationHandler : INotificationHandler<UpdateProductInfoNotification>
{
    IBasketDal _basketDal;

    public UpdateProductInfoNotificationHandler(IBasketDal basketDal)
    {
        _basketDal = basketDal;
    }

    public async Task Handle(UpdateProductInfoNotification notification, CancellationToken cancellationToken)
    {
        var index = 0;
        while (true)
        {
            var datas = await _basketDal.GetListAsync(w => w.Items.Any(q => q.ProductId == notification.ProductId), index: 10, size: 100);

            foreach (var item in datas.Items.SelectMany(q => q.Items.Where(e => e.ProductId == notification.ProductId)))
            {
                item.ProductName = notification.Name;
                item.UnitPrice = notification.UnitPrice;
            }
            await _basketDal.UpdateRangeAsync(datas.Items);
            if (datas.Count < 100)
                break;

            index++;
        }
    }
}
