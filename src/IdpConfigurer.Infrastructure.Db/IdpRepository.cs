using Dapper;
using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Idp;
using IdpConfigurer.Infrastructure.Db.Dbo;
using IdpConfigurer.Specification;

namespace IdpConfigurer.Infrastructure.Db
{
    public class IdpRepository : IIdpApi
    {
        private readonly ConnectionProvider _connectionProvider;

        public IdpRepository(ConnectionProvider connectionProvider) => _connectionProvider = connectionProvider;

        public async Task<Idp> CreateIdpAsync(CreateIdpParam param, CancellationToken cancellationToken)
        {
            var con = _connectionProvider.Connection();

            var dbo = param.ToDbo();

            var dp = new DynamicParameters();
            dp.Add("@Name", dbo.Name);
            dp.Add("@Uri", dbo.Uri);
            dp.Add("@json", dbo.Json);

            var result = await con.QueryAsync<IdpDbo>("idpc.CreateIdp", dp, commandType: System.Data.CommandType.StoredProcedure);
            var retDbo = result.First();
            var retVal = retDbo.ToDto();
            return retVal;
        }

        public async Task DeleteIdpAsync(DeleteIdpParam param, CancellationToken cancellationToken)
        {
            var con = _connectionProvider.Connection();

            var dp = new DynamicParameters();
            dp.Add("@Name", param.Name);

            await con.ExecuteAsync("idpc.DeleteIdp", dp, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Idp> ReadIdpAsync(ReadIdpParam param, CancellationToken cancellationToken)
        {
            var con = _connectionProvider.Connection();

            var dp = new DynamicParameters();
            dp.Add("@Name", param.Name);

            var result = await con.QueryAsync<IdpDbo>("idpc.ReadIdp", dp, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
            var retDbo = result.First();
            var retVal = retDbo.ToDto();
            return retVal;
        }

        public async Task<IEnumerable<Idp>> ReadIdpsAsync(CancellationToken cancellationToken)
        {
            var con = _connectionProvider.Connection();

            var result = await con.QueryAsync<IdpDbo>("idpc.ReadIdps", commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
            var retVal = result.Select(e => e.ToDto());
            return retVal;
        }

        public async Task<Idp> UpdateIdpAsync(UpdateIdpParam param, CancellationToken cancellationToken)
        {
            var con = _connectionProvider.Connection();

            var dbo = param.Idp.ToDbo();

            var dp = new DynamicParameters();
            dp.Add("@Name", dbo.Name);
            if (param.IdpName != null) 
                dp.Add("@newName", param.IdpName);
            else
                dp.Add("@newName", null);
            dp.Add("@Uri", dbo.Uri);
            dp.Add("@json", dbo.Json);

            var result = await con.QueryAsync<IdpDbo>("idpc.UpdateIdp", dp, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
            var retDbo = result.First();
            var retVal = retDbo.ToDto();
            return retVal;
        }

    }

}
