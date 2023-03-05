using System.Net.Http.Json;

namespace IdpConfigurer.Infrastructure.WebApi.Clients;

public abstract class HttpClientApiBase<T> where T : class
{
    protected abstract HttpClient GetHttpClient();

    protected async Task<T> PostAsync<P>(P p, CancellationToken ct) where P : class =>
        await PostAsync(string.Empty, p, ct).ConfigureAwait(false);

    protected async Task<T> PostAsync<P>(string uri, P p, CancellationToken ct) where P : class
    {
        var httpClient = GetHttpClient();
        using var res = await httpClient.PostAsJsonAsync(uri, p, ct).ConfigureAwait(false);
        res.EnsureSuccessStatusCode();
        var retVal = await res.Content.ReadFromJsonAsync<T>(cancellationToken: ct).ConfigureAwait(false);
        if (retVal == null) throw new Exception("Request produced no data");
        return retVal;
    }

    protected async Task DeleteAsync(string uri, CancellationToken ct)
    {
        var httpClient = GetHttpClient();
        using var res = await httpClient.DeleteAsync(uri, ct).ConfigureAwait(false);
        res.EnsureSuccessStatusCode();
    }

    protected async Task<IEnumerable<T>> GetAsync(CancellationToken ct) =>
        await GetAsync(string.Empty, ct).ConfigureAwait(false);

    protected async Task<IEnumerable<T>> GetAsync(string uri, CancellationToken ct)
    {
        var httpClient = GetHttpClient();
        using var res = await httpClient.GetAsync(uri, ct).ConfigureAwait(false);
        res.EnsureSuccessStatusCode();
        var retVal = await res.Content.ReadFromJsonAsync<IEnumerable<T>>(cancellationToken: ct).ConfigureAwait(false);
        if (retVal == null) throw new Exception("Request produced no data");
        return retVal;
    }

    protected async Task<T> PutAsync<P>(P p, CancellationToken ct) where P : class =>
        await PutAsync(string.Empty, p, ct).ConfigureAwait(false);

    protected async Task<T> PutAsync<P>(string uri, P p, CancellationToken ct) where P : class
    {
        var httpClient = GetHttpClient();
        using var res = await httpClient.PutAsJsonAsync(uri, p, ct).ConfigureAwait(false);
        res.EnsureSuccessStatusCode();
        var retVal = await res.Content.ReadFromJsonAsync<T>(cancellationToken: ct).ConfigureAwait(false);
        if (retVal == null) throw new Exception("Request produced no data");
        return retVal;
    }

}

public abstract class NamedHttpClientApiBase<T> : HttpClientApiBase<T> where T : class
{
    private readonly IHttpClientFactory _httpClientFactory;

    protected NamedHttpClientApiBase(IHttpClientFactory httpClientFactory) => 
        _httpClientFactory = httpClientFactory;

    protected override HttpClient GetHttpClient() => 
        _httpClientFactory.CreateClient(GetType().Name);
}

public abstract class TypedHttpClientApiBase<T> : HttpClientApiBase<T> where T : class
{
    private readonly HttpClient _httpClient;

    protected TypedHttpClientApiBase(HttpClient httpClient) => 
        _httpClient = httpClient;

    protected override HttpClient GetHttpClient() => _httpClient;
}
