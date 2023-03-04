using System.Net.Http.Json;

namespace IdpConfigurer.Infrastructure.WebApi.Clients;

public abstract class HttpClientApiBase<T> where T : class
{
    protected readonly IHttpClientFactory _httpClientFactory;

    protected HttpClientApiBase(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    protected async Task<T> DoPostAsync<P>(P p, CancellationToken ct) where P : class
        => await DoPutAsync(string.Empty, p, ct).ConfigureAwait(false);

    protected async Task<T> DoPostAsync<P>(string uri, P p, CancellationToken ct) where P : class
    {
        var httpClient = GetHttpClient();
        using var res = await httpClient.PostAsJsonAsync(uri, p, ct).ConfigureAwait(false);
        res.EnsureSuccessStatusCode();
        var retVal = await res.Content.ReadFromJsonAsync<T>(cancellationToken: ct).ConfigureAwait(false);
        if (retVal == null) throw new Exception("Request produced no data");
        return retVal;
    }

    protected async Task DoDeleteAsync(string uri, CancellationToken ct)
    {
        var httpClient = GetHttpClient();
        using var res = await httpClient.DeleteAsync(uri, ct).ConfigureAwait(false);
        res.EnsureSuccessStatusCode();
    }

    protected async Task<IEnumerable<T>> DoReadAsync(CancellationToken ct)
        => await DoReadAsync(string.Empty, ct).ConfigureAwait(false);

    protected async Task<IEnumerable<T>> DoReadAsync(string uri, CancellationToken ct)
    {
        var httpClient = GetHttpClient();
        using var res = await httpClient.GetAsync(uri, ct).ConfigureAwait(false);
        res.EnsureSuccessStatusCode();
        var retVal = await res.Content.ReadFromJsonAsync<IEnumerable<T>>(cancellationToken: ct).ConfigureAwait(false);
        if (retVal == null) throw new Exception("Request produced no data");
        return retVal;
    }

    protected async Task<T> DoPutAsync<P>(string uri, P p, CancellationToken ct) where P : class
    {
        var httpClient = GetHttpClient();
        using var res = await httpClient.PutAsJsonAsync(uri, p, ct).ConfigureAwait(false);
        res.EnsureSuccessStatusCode();
        var retVal = await res.Content.ReadFromJsonAsync<T>(cancellationToken: ct).ConfigureAwait(false);
        if (retVal == null) throw new Exception("Request produced no data");
        return retVal;
    }

    private HttpClient GetHttpClient() => _httpClientFactory.CreateClient(GetType().Name);

}
