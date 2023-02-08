using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdpConfigurer.Infrastructure.Db
{
    public class ApiRepository : IApiScopeApi
    {
        private readonly ConnectionProvider _connectionProvider;

        public ApiRepository(ConnectionProvider connectionProvider) => _connectionProvider = connectionProvider;

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
}
