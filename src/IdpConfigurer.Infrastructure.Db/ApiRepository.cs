using Dapper;
using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Specification;

namespace IdpConfigurer.Infrastructure.Db
{
    public class ApiRepository : IApiScopeApi
    {
        private readonly ConnectionProvider _connectionProvider;

        public ApiRepository(ConnectionProvider connectionProvider) => _connectionProvider = connectionProvider;

        public async Task<ApiScope> CreateApiScopeAsync(CreateApiScopeParam param, CancellationToken cancellationToken)
        {
            using var con = _connectionProvider.Connection;

            var dp = new DynamicParameters();
            dp.Add("@ipdName", param.IdpName);
            dp.Add("@Name", param.Name);
            dp.Add("@displayName", param.DisplayName);

            var retVal = await con.QueryAsync<ApiScope>("idpc.CreateApi", dp, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
            return retVal.First();
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
