using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityModelManager.Specification;

public interface IApiScopeApi
{
    Task<ApiScope> CreateApiScopeAsync(CreateApiScopeParam param, CancellationToken cancellationToken);
    Task<IEnumerable<ApiScope>> ReadApiScopesAsync(ReadApiScopesParam param, CancellationToken cancellationToken);
    Task<ApiScope> ReadApiScopeAsync(ReadApiScopeParam param, CancellationToken cancellationToken);
    Task<ApiScope> UpdateApiScopeAsync(UpdateApiScopeParam param, CancellationToken cancellationToken);
    Task<bool> DeleteApiScopeAsync(DeleteApiScopeParam param, CancellationToken cancellationToken);
}
