﻿using Dapper;
using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Specification.Api;

namespace IdpConfigurer.Infrastructure.Db;

public class ApiScopeRepository : IApiScopeApi
{
    private readonly ConnectionProvider _connectionProvider;

    public ApiScopeRepository(ConnectionProvider connectionProvider) => _connectionProvider = connectionProvider;

    public async Task<ApiScope> CreateApiScopeAsync(CreateApiScopeParam param, CancellationToken cancellationToken)
    {
        using var con = _connectionProvider.Connection();

        var dp = new DynamicParameters();
        dp.Add("@IdpName", param.IdpName);
        dp.Add("@Name", param.Name);
        dp.Add("@displayName", param.DisplayName);

        var result = await con.QueryAsync<ApiScope>("idpc.CreateApi", dp, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
        return result.First();
    }

    public async Task DeleteApiScopeAsync(DeleteApiScopeParam param, CancellationToken cancellationToken)
    {
        using var con = _connectionProvider.Connection();

        var dp = new DynamicParameters();
        dp.Add("@IdpName", param.IdpName);
        dp.Add("@Name", param.Name);

        await con.ExecuteAsync("idpc.DeleteApi", dp, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
    }

    public async Task<IEnumerable<ApiScope>> ReadApiScopeAsync(ReadApiScopeParam param, CancellationToken cancellationToken)
    {
        using var con = _connectionProvider.Connection();

        var dp = new DynamicParameters();
        dp.Add("@IdpName", param.IdpName);
        dp.Add("@Name", param.Name);

        var retVal = await con.QueryAsync<ApiScope>("idpc.ReadApi", dp, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
        return retVal;
    }

    public async Task<IEnumerable<ApiScope>> ReadApiScopesAsync(ReadApiScopesParam param, CancellationToken cancellationToken)
    {
        using var con = _connectionProvider.Connection();

        var dp = new DynamicParameters();
        dp.Add("@IdpName", param.IdpName);

        var retVal = await con.QueryAsync<ApiScope>("idpc.ReadApis", dp, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
        return retVal;
    }

    public async Task<ApiScope> UpdateApiScopeAsync(UpdateApiScopeParam param, CancellationToken cancellationToken)
    {
        using var con = _connectionProvider.Connection();

        var dp = new DynamicParameters();
        dp.Add("@IdpName", param.IdpName);
        dp.Add("@Name", param.ApiScope.Name);
        dp.Add("@displayName", param.ApiScope.DisplayName);

        var result = await con.QueryAsync<ApiScope>("idpc.UpdateApi", dp, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
        return result.First();
    }

}
