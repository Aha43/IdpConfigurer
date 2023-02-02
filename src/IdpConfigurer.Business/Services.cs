using IdentityModelManager.Infrastructure.Memory;
using IdentityModelManager.Specification;
using IdpConfigurer.Business.ViewController;
using Microsoft.Extensions.DependencyInjection;

namespace IdpConfigurer.Business;

public static class Services
{
    public static IServiceCollection AddIdentityModelManagerInMemoryServices(this IServiceCollection services)
    {
        return services
            .AddApis()
            .AddViewControllers();
    }

    private static IServiceCollection AddApis(this IServiceCollection services)
    {
        return services
            .AddSingleton<IIdpApi, InMemoryIdpRepository>()
            .AddSingleton<IClientApi, InMemoryClientRepository>()
            .AddSingleton<IApiScopeApi, InMemoryApiScopeApi>();
    }

    private static IServiceCollection AddViewControllers(this IServiceCollection services)
    {
        return services
            .AddSingleton<IdpViewController>()
            .AddSingleton<IdpsViewController>()
            .AddSingleton<ClientViewController>();
    }

}
