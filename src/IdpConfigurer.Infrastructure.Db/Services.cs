using IdpConfigurer.Specification.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using System.Data.SqlClient;

namespace IdpConfigurer.Infrastructure.Db;

public static class Services
{
    public static IServiceCollection AddDbInfrastructure(this IServiceCollection services, IConfiguration configration)
    {
        var connectionString = configration.GetConnectionString("default");
        if (connectionString == null) return services;

        return services.AddSingleton(new ConnectionProvider(connectionString))
            .AddApis();
    }

    private static IServiceCollection AddApis(this IServiceCollection services)
    {
        return services.AddSingleton<IIdpApi, IdpRepository>()
            .AddSingleton<IApiScopeApi, ApiScopeRepository>()
            .AddSingleton<IClientApi, ClientRepository>();
    }
}

public class ConnectionProvider
{
    private readonly string _connectionString;

    public ConnectionProvider(string connectionString) => _connectionString = connectionString;

    public DbConnection Connection()
    {
        return new SqlConnection(_connectionString);
    }
}
