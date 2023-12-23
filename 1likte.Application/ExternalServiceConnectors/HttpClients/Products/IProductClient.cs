using _1likte.Domain.ApiModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1likte.Application.ExternalServiceConnectors.HttpClients.Products;

public interface IProductClient
{
    Task<ProductInfoResponse> GetProductInfo(Guid id);
}
