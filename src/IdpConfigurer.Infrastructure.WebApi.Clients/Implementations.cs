using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
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
        await DoGetAsync($"{p.Name}", ct).ConfigureAwait(false);

    public async Task<IEnumerable<Idp>> ReadIdpsAsync(CancellationToken ct) => 
        await DoGetAsync(ct).ConfigureAwait(false);

    public async Task<Idp> UpdateIdpAsync(UpdateIdpParam p, CancellationToken ct) => 
        await DoPutAsync("update", p, ct).ConfigureAwait(false);

}

public class ClientWebApiClient : HttpClientApiBase<Client>, IClientApi
{
    public ClientWebApiClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

    public async Task<Client> CreateClientAsync(CreateClientParam p, CancellationToken ct) =>
        await DoPostAsync(p, ct).ConfigureAwait(false);

    public async Task DeleteClientAsync(DeleteClientParam p, CancellationToken ct) => 
        await DoDeleteAsync($"delete/{p.IdpName}/{p.ClientId}", ct).ConfigureAwait(false);

    public async Task<IEnumerable<Client>> ReadClientAsync(ReadClientParam p, CancellationToken ct) => 
        await DoGetAsync($"{p.IdpName}/{p.ClientId}", ct).ConfigureAwait(false);

    public async Task<IEnumerable<Client>> ReadClientsAsync(ReadClientsParam p, CancellationToken ct) => 
        await DoGetAsync($"{p.IdpName}", ct).ConfigureAwait(false);

    public async Task<Client> UpdateClientAsync(UpdateClientParam p, CancellationToken ct)
    {
        return await DoPutAsync("update", p, ct).ConfigureAwait(false);
    }

}

public class ApiScopeWebApiClient : HttpClientApiBase<ApiScope>, IApiScopeApi
{
    public ApiScopeWebApiClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

    public async Task<ApiScope> CreateApiScopeAsync(CreateApiScopeParam p, CancellationToken ct) => 
        await DoPostAsync(p, ct).ConfigureAwait(false);

    public async Task DeleteApiScopeAsync(DeleteApiScopeParam p, CancellationToken ct) => 
        await DoDeleteAsync($"delete/{p.IdpName}/{p.Name}", ct).ConfigureAwait(false);

    public async Task<IEnumerable<ApiScope>> ReadApiScopeAsync(ReadApiScopeParam p, CancellationToken ct)
        => await DoGetAsync($"{p.IdpName}/{p.Name}", ct).ConfigureAwait(false);

    public async Task<IEnumerable<ApiScope>> ReadApiScopesAsync(ReadApiScopesParam p, CancellationToken ct)
        => await DoGetAsync($"{p.IdpName}", ct).ConfigureAwait(false);

    public async Task<ApiScope> UpdateApiScopeAsync(UpdateApiScopeParam p, CancellationToken ct) => 
        await DoPutAsync("update", p, ct).ConfigureAwait(false);

}
