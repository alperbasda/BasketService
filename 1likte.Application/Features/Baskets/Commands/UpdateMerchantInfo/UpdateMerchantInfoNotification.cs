using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1likte.Application.Features.Baskets.Commands.UpdateMerchantInfo;

public  class UpdateMerchantInfoNotification : INotification
{
    public Guid MerchantId { get; set; }

    public string Name { get; set; }
}
