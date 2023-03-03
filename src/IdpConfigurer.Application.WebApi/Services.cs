using IdpConfigurer.Infrastructure.Db;
using IdpConfigurer.Infrastructure.Memory;

namespace IdpConfigurer.Application.WebApi;

public static class Services
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) 
    {
        var api = configuration["Infrastructure"];
        api ??= "memory";
        return api switch
        {
            "memory" => services.AddInMemoryApis(),
            "db" => services.AddDbInfrastructure(configuration),
            _ => throw new ArgumentException($"Unknown infrastructure '{api}'"),
        };
    }

}
