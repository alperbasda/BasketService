using _1likte.Application.Contracts;
using _1likte.Application.Features.Baskets.Commands.UpdateMerchantInfo;
using MediatR;

namespace _1likte.Application.Features.Baskets.Handlers.Commands.UpdateMerchantInfo;

public class UpdateMerchantInfoNotificationHandler : INotificationHandler<UpdateMerchantInfoNotification>
{
    IBasketDal _basketDal;

    public UpdateMerchantInfoNotificationHandler(IBasketDal basketDal)
    {
        _basketDal = basketDal;
    }

    public async Task Handle(UpdateMerchantInfoNotification notification, CancellationToken cancellationToken)
    {
        var index = 0;
        while (true)
        {
            var datas = await _basketDal.GetListAsync(w => w.Items.Any(q => q.MerchantId == notification.MerchantId), index: 10, size: 100);

            foreach (var item in datas.Items.SelectMany(q => q.Items.Where(e => e.MerchantId == notification.MerchantId)))
            {
                item.ProductName = notification.Name;
            }

            await _basketDal.UpdateRangeAsync(datas.Items);
            if (datas.Count < 100)
                break;

            index++;
        }
    }
}
