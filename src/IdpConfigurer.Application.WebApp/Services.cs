using IdpConfigurer.Business;

namespace IdpConfigurer.Application.WebApp;

public static class Services
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services.AddIdentityModelManagerInMemoryServices();
    }
}
