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
        return services.AddSingleton(new ConnectionProvider(connectionString));
    }
}

public class ConnectionProvider
{
    private readonly string _connectionString;

    public ConnectionProvider(string connectionString) => _connectionString = connectionString;

    public DbConnection Connection => new SqlConnection(_connectionString);
}
