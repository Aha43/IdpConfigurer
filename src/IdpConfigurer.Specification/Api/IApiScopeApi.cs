using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;

namespace IdpConfigurer.Specification.Api;

public interface IApiScopeApi
{
    Task<ApiScope> CreateApiScopeAsync(CreateApiScopeParam param, CancellationToken cancellationToken);
    Task<IEnumerable<ApiScope>> ReadApiScopesAsync(ReadApiScopesParam param, CancellationToken cancellationToken);
    Task<ApiScope> ReadApiScopeAsync(ReadApiScopeParam param, CancellationToken cancellationToken);
    Task<ApiScope> UpdateApiScopeAsync(UpdateApiScopeParam param, CancellationToken cancellationToken);
    Task DeleteApiScopeAsync(DeleteApiScopeParam param, CancellationToken cancellationToken);
}
