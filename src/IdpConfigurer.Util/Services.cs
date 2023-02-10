using IdpConfigurer.Specification.Tool;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdpConfigurer.Util;

public static class Services
{
    public static IServiceCollection AddUtilities(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton<ISharedGenerator, Sha256SharedSecretGenerator>();
    }
}
