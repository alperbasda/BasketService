using _1likte.Domain.ApiModels.Merchants;

namespace _1likte.Application.ExternalServiceConnectors.HttpClients.Merchants;

public class MerchantClient : IMerchantClient
{
    public Task<MerchantInfoResponse> GetMerchantInfo(Guid id)
    {
        // Api Requests ....

        //Mock Data
        return Task.FromResult(new MerchantInfoResponse
        {
            Id = id,
            Name = "1likte"
        });
    }
}
