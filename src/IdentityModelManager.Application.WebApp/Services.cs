using IdpConfigurer.Business;

namespace IdentityModelManager.Application.WebApp;

public static class Services
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services.AddIdentityModelManagerInMemoryServices();
    }
}
