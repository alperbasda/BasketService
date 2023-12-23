using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1likte.Application.Features.Baskets.Commands.UpdateProductInfo;

public class UpdateProductInfoNotification : INotification
{
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public decimal UnitPrice { get; set; }

}
