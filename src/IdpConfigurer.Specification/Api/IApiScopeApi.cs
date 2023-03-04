using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;

namespace IdpConfigurer.Specification.Api;

public interface IApiScopeApi
{
    Task<ApiScope> CreateApiScopeAsync(CreateApiScopeParam p, CancellationToken ct);
    Task<IEnumerable<ApiScope>> ReadApiScopesAsync(ReadApiScopesParam p, CancellationToken ct);
    Task<IEnumerable<ApiScope>> ReadApiScopeAsync(ReadApiScopeParam p, CancellationToken ct);
    Task<ApiScope> UpdateApiScopeAsync(UpdateApiScopeParam p, CancellationToken ct);
    Task DeleteApiScopeAsync(DeleteApiScopeParam p, CancellationToken ct);
}
