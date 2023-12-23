using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1likte.Events.Products;

public class ProductInfoChangedEvent
{
    public Guid ProductId { get; set; }
    
    public string Name { get; set; }

    public decimal UnitPrice { get; set; }
}
