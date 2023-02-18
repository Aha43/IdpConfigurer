using IdpConfigurer.Business.ViewController;
using IdpConfigurer.Domain;
using IdpConfigurer.Infrastructure.Db;
using IdpConfigurer.Infrastructure.Memory;
using IdpConfigurer.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdpConfigurer.Business;

public static class Services
{
    public static IServiceCollection AddIdpConfigurerServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddInfrastructure(configuration)
            .AddViewControllers()
            .AddCustomIdpDataDefinition(configuration)
            .AddUtilities(configuration);
    }

    private static IServiceCollection AddCustomIdpDataDefinition(this IServiceCollection services, IConfiguration configuration)
    {
        var definitions = configuration.GetSectionAs<IdpCustomDataDefinition>();
        if (definitions == null) definitions = new();
        return services.AddSingleton(definitions);
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        var api = configuration["Infrastructure"];
        api = (api == null) ? "memory" : api;
        switch (api) 
        {
            case "memory": return services.AddInMemoryApis();
            case "db": return services.AddDbInfrastructure(configuration);
            default: throw new ArgumentException($"Unknown infrastructure '{api}'");
        }
    }  

    private static IServiceCollection AddViewControllers(this IServiceCollection services)
    {
        return services
            .AddSingleton<IdpViewController>()
            .AddSingleton<IdpsViewController>()
            .AddSingleton<ClientViewController>()
            .AddSingleton<ApiScopeViewController>();
    }

}
