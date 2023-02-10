using IdpConfigurer.Specification.Api;
using Microsoft.Extensions.DependencyInjection;

namespace IdpConfigurer.Infrastructure.Memory
{
    public static class Services
    {
        public static IServiceCollection AddInMemoryApis(this IServiceCollection services)
        {
            return services
                .AddSingleton<IIdpApi, InMemoryIdpRepository>()
                .AddSingleton<IClientApi, InMemoryClientRepository>()
                .AddSingleton<IApiScopeApi, InMemoryApiScopeApi>();
        }
    }
}
