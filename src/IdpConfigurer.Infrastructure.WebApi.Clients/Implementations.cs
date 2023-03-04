using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Client;
using IdpConfigurer.Domain.Param.Idp;
using IdpConfigurer.Specification.Api;

namespace IdpConfigurer.Infrastructure.WebApi.Clients;

public class IdpWebApiClient : HttpClientApiBase<Idp>, IIdpApi
{
    public IdpWebApiClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

    public async Task<Idp> CreateIdpAsync(CreateIdpParam p, CancellationToken ct) => 
        await DoPostAsync(p, ct).ConfigureAwait(false);

    public async Task DeleteIdpAsync(DeleteIdpParam p, CancellationToken ct) => 
        await DoDeleteAsync($"delete/{p.Name}", ct).ConfigureAwait(false);

    public async Task<IEnumerable<Idp>> ReadIdpAsync(ReadIdpParam p, CancellationToken ct) => 
        await DoReadAsync($"{p.Name}", ct).ConfigureAwait(false);

    public async Task<IEnumerable<Idp>> ReadIdpsAsync(CancellationToken ct) => 
        await DoReadAsync(ct).ConfigureAwait(false);

    public async Task<Idp> UpdateIdpAsync(UpdateIdpParam p, CancellationToken ct) => 
        await DoPutAsync("update", p, ct).ConfigureAwait(false);

}

public class ClientWebApiClient : HttpClientApiBase<Client>, IClientApi
{
    public ClientWebApiClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public async Task<Client> CreateClientAsync(CreateClientParam p, CancellationToken ct) =>
        await DoPostAsync(p, ct).ConfigureAwait(false);

    public Task DeleteClientAsync(DeleteClientParam p, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Client>> ReadClientAsync(ReadClientParam p, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Client>> ReadClientsAsync(ReadClientsParam p, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Client> UpdateClientAsync(UpdateClientParam p, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
