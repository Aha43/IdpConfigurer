using Dapper;
using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Client;
using IdpConfigurer.Infrastructure.Db.Dbo;
using IdpConfigurer.Specification.Api;

namespace IdpConfigurer.Infrastructure.Db;

public class ClientRepository : IClientApi
{
    private readonly ConnectionProvider _connectionProvider;

    public ClientRepository(ConnectionProvider connectionProvider) => _connectionProvider = connectionProvider;

    public async Task<Client> CreateClientAsync(CreateClientParam param, CancellationToken cancellationToken)
    {
        using var con = _connectionProvider.Connection();

        var dbo = param.ToDbo();

        var dp = new DynamicParameters();
        dp.Add("@idpName", dbo.IdpName);
        dp.Add("@clientId", dbo.ClientId);
        dp.Add("@clientName", dbo.ClientName);
        dp.Add("@json", dbo.Json);

        var result = await con.QueryAsync<ClientDbo>("idpc.CreateClient", dp, commandType: System.Data.CommandType.StoredProcedure);
        var retDbo = result.First();
        var retVal = retDbo.ToDto();
        return retVal;
    }

    public async Task DeleteClientAsync(DeleteClientParam param, CancellationToken cancellationToken)
    {
        using var con = _connectionProvider.Connection();

        var dp = new DynamicParameters();
        dp.Add("@idpName", param.IdpName);
        dp.Add("@clientId", param.ClientId);

        await con.ExecuteAsync("idpc.DeleteClient", dp, commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Client>> ReadClientAsync(ReadClientParam param, CancellationToken cancellationToken)
    {
        using var con = _connectionProvider.Connection();

        var dp = new DynamicParameters();
        dp.Add("@idpName", param.IdpName);
        dp.Add("@clientId", param.ClientId);

        var result = await con.QueryAsync<ClientDbo>("idpc.ReadClient", dp, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
        var retVal = result.Select(e => e.ToDto());
        return retVal;
    }

    public async Task<IEnumerable<Client>> ReadClientsAsync(ReadClientsParam param, CancellationToken cancellationToken)
    {
        using var con = _connectionProvider.Connection();

        var dp = new DynamicParameters();
        dp.Add("@idpName", param.IdpName);

        var result = await con.QueryAsync<ClientDbo>("idpc.ReadClients", dp, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
        var retVal = result.Select(e => e.ToDto());
        return retVal;
    }

    public async Task<Client> UpdateClientAsync(UpdateClientParam param, CancellationToken cancellationToken)
    {
        using var con = _connectionProvider.Connection();

        var dbo = param.Client.ToDbo(param.IdpName);

        var dp = new DynamicParameters();
        dp.Add("@IdpName", dbo.IdpName);
        dp.Add("@clientId", dbo.ClientId);
        dp.Add("@clientName", dbo.ClientName);
        dp.Add("@json", dbo.Json);

        var result = await con.QueryAsync<ClientDbo>("idpc.UpdateClient", dp, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
        var retDbo = result.First();
        var retVal = retDbo.ToDto();
        return retVal;
    }

}
