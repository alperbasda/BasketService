using _1likte.Domain.ApiModels.Products;

namespace _1likte.Application.ExternalServiceConnectors.HttpClients.Products;

public class ProductClient : IProductClient
{
    public Task<ProductInfoResponse> GetProductInfo(Guid id)
    {
        // Api Requests ....

        //Mock Data
        return Task.FromResult(new ProductInfoResponse
        {
            Id = id,
            UnitPrice = 100,
            Name = "Klavye"
        });
    }
}
