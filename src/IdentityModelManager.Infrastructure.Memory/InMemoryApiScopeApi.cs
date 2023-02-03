using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Specification;

namespace IdentityModelManager.Infrastructure.Memory;

public class InMemoryApiScopeApi : IApiScopeApi
{
    private readonly Dictionary<ApiScopeKey, ApiScope> _scopes = new();

    public Task<ApiScope> CreateApiScopeAsync(CreateApiScopeParam param, CancellationToken cancellationToken)
    {
        var key = new ApiScopeKey(param);
        if (_scopes.ContainsKey(key))
        {
            throw new ArgumentException($"ApiScope exists for key: '{key}'");
        }

        _scopes[key] = new ApiScope { Name = param.Name, DisplayName = param.DisplayName };
        return Task.FromResult(_scopes[key]);
    }

    public Task<bool> DeleteApiScopeAsync(DeleteApiScopeParam param, CancellationToken cancellationToken)
    {
        var key = new ApiScopeKey(param);
        if (_scopes.ContainsKey(key))
        {
            _scopes.Remove(key);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public Task<ApiScope> ReadApiScopeAsync(ReadApiScopeParam param, CancellationToken cancellationToken)
    {
        var key = new ApiScopeKey(param);
        if (_scopes.TryGetValue(key, out ApiScope? apiScope))
        {
            return Task.FromResult(apiScope);
        }

        throw new ArgumentException($"ApiScope does not exist for key: '{key}'");
    }

    public Task<IEnumerable<ApiScope>> ReadApiScopesAsync(ReadApiScopesParam param, CancellationToken cancellationToken)
    {
        var retValues = _scopes.Where(e => e.Key.IdpName.Equals(param.IdpName)).Select(e => e.Value);
        return Task.FromResult(retValues);
    }

    public Task<ApiScope> UpdateApiScopeAsync(UpdateApiScopeParam param, CancellationToken cancellationToken)
    {
        var key = new ApiScopeKey(param.IdpName, param.ApiScope.Name);
        if (_scopes.ContainsKey(key))
        {
            _scopes[key] = param.ApiScope;
            return Task.FromResult(param.ApiScope);
        }

        throw new ArgumentException($"ApiScope does not exist for key: '{key}'");
    }

}

internal record class ApiScopeKey
{
    public string IdpName { get; private set; }
    public string Name { get; private set; }

    public ApiScopeKey(string idpName, string name)
    {
        IdpName = idpName;
        Name = name;
    }

    public ApiScopeKey(dynamic o)
    {
        IdpName = o.IdpName;
        Name = o.Name;
    }
    
}

