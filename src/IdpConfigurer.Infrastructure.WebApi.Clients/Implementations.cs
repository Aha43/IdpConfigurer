using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Domain.Param.Client;
using IdpConfigurer.Domain.Param.Idp;
using IdpConfigurer.Specification.Api;

namespace IdpConfigurer.Infrastructure.WebApi.Clients;

public class IdpWebApiClient : TypedHttpClientApiBase<Idp>, IIdpApi
{
    public IdpWebApiClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<Idp> CreateIdpAsync(CreateIdpParam p, CancellationToken ct) => 
        await PostAsync(p, ct).ConfigureAwait(false);

    public async Task DeleteIdpAsync(DeleteIdpParam p, CancellationToken ct) => 
        await DeleteAsync(p.Name, ct).ConfigureAwait(false);

    public async Task<IEnumerable<Idp>> ReadIdpAsync(ReadIdpParam p, CancellationToken ct) => 
        await GetAsync(p.Name, ct).ConfigureAwait(false);

    public async Task<IEnumerable<Idp>> ReadIdpsAsync(CancellationToken ct) => 
        await GetAsync(ct).ConfigureAwait(false);

    public async Task<Idp> UpdateIdpAsync(UpdateIdpParam p, CancellationToken ct) => 
        await PutAsync(p, ct).ConfigureAwait(false);
}

public class ClientWebApiClient : TypedHttpClientApiBase<Client>, IClientApi
{
    public ClientWebApiClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<Client> CreateClientAsync(CreateClientParam p, CancellationToken ct) =>
        await PostAsync(p, ct).ConfigureAwait(false);

    public async Task DeleteClientAsync(DeleteClientParam p, CancellationToken ct) => 
        await DeleteAsync($"{p.IdpName}/{p.ClientId}", ct).ConfigureAwait(false);

    public async Task<IEnumerable<Client>> ReadClientAsync(ReadClientParam p, CancellationToken ct) => 
        await GetAsync($"{p.IdpName}/{p.ClientId}", ct).ConfigureAwait(false);

    public async Task<IEnumerable<Client>> ReadClientsAsync(ReadClientsParam p, CancellationToken ct) => 
        await GetAsync(p.IdpName, ct).ConfigureAwait(false);

    public async Task<Client> UpdateClientAsync(UpdateClientParam p, CancellationToken ct) =>
        await PutAsync(p, ct).ConfigureAwait(false);
}

public class ApiScopeWebApiClient : TypedHttpClientApiBase<ApiScope>, IApiScopeApi
{
    public ApiScopeWebApiClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<ApiScope> CreateApiScopeAsync(CreateApiScopeParam p, CancellationToken ct) => 
        await PostAsync(p, ct).ConfigureAwait(false);

    public async Task DeleteApiScopeAsync(DeleteApiScopeParam p, CancellationToken ct) => 
        await DeleteAsync($"{p.IdpName}/{p.Name}", ct).ConfigureAwait(false);

    public async Task<IEnumerable<ApiScope>> ReadApiScopeAsync(ReadApiScopeParam p, CancellationToken ct) =>
        await GetAsync($"{p.IdpName}/{p.Name}", ct).ConfigureAwait(false);

    public async Task<IEnumerable<ApiScope>> ReadApiScopesAsync(ReadApiScopesParam p, CancellationToken ct) =>
        await GetAsync(p.IdpName, ct).ConfigureAwait(false);

    public async Task<ApiScope> UpdateApiScopeAsync(UpdateApiScopeParam p, CancellationToken ct) => 
        await PutAsync(p, ct).ConfigureAwait(false);
}
