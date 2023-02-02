using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.ApiScope;
using IdentityModelManager.Specification;

namespace IdentityModelManager.Infrastructure.Memory;

public class InMemoryApiScopeApi : IApiScopeApi
{
    public Task<ApiScope> CreateApiScopeAsync(CreateApiScopeParam param, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteApiScopeAsync(DeleteApiScopeParam param, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiScope> ReadApiScopeAsync(ReadApiScopeParam param, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ApiScope>> ReadApiScopesAsync(ReadApiScopesParam param, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiScope> UpdateApiScopeAsync(UpdateApiScopeParam param, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
