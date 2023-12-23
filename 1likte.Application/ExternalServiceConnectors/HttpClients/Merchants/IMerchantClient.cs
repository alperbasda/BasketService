using _1likte.Domain.ApiModels.Merchants;

namespace _1likte.Application.ExternalServiceConnectors.HttpClients.Merchants;

public interface IMerchantClient
{
    Task<MerchantInfoResponse> GetMerchantInfo(Guid id);
}
